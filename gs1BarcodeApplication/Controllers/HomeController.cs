using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXing;
using gs1BarcodeApplication.Models;
using System.Drawing;
using System.Drawing.Imaging;
using QRCoder;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using gs1BarcodeApplication.Helpers;


namespace gs1BarcodeApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Submit(List<Gs1FieldInput> inputs)
        {
            if (inputs == null || inputs.Count == 0)
            {
                ModelState.AddModelError("", "No inputs received.");
                return View("Index", new List<Gs1FieldInput>());
            }
            var model = new ProductLabelModel();
            
            foreach( var input in inputs)
            {

                var prop = typeof(ProductLabelModel).GetProperty(input.Field);
                if(prop != null && prop.CanWrite)
                {
                    prop.SetValue(model, input.Value);
                }
            }
            string gs1Data = Gs1Builder.BuildGs1Data(model);
            string barcodeBase64 = GenerateBarcode(gs1Data);
            string qrImage = GenerateQrBase64(gs1Data);
            ViewBag.Gs1String = gs1Data;
            ViewBag.BarcodeImage = "data:image/png;base64," + barcodeBase64;
            ViewBag.QrImage = qrImage;
            return View("LabelResult", model);
        }
        [HttpPost]
        public FileResult ExportPdf(string gs1String, string barcodeBase64, string qrBase64)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A6);
                PdfWriter.GetInstance(document, ms);
                document.Open();

                // Fonts
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                var textFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                // Title
                document.Add(new Paragraph("GS1 Label", titleFont));
                document.Add(new Paragraph(" "));

                // GS1 String
                document.Add(new Paragraph("GS1 Data:", textFont));
                document.Add(new Paragraph(gs1String, textFont));
                document.Add(new Paragraph(" "));

                // Barcode Image
                if (!string.IsNullOrEmpty(barcodeBase64))
                {
                    byte[] barcodeBytes = Convert.FromBase64String(barcodeBase64.Replace("data:image/png;base64,", ""));
                    iTextSharp.text.Image barcodeImg = iTextSharp.text.Image.GetInstance(barcodeBytes);
                    barcodeImg.ScaleToFit(200f, 50f);
                    document.Add(barcodeImg);
                }

                document.Add(new Paragraph(" "));

                // QR Image
                if (!string.IsNullOrEmpty(qrBase64))
                {
                    byte[] qrBytes = Convert.FromBase64String(qrBase64.Replace("data:image/png;base64,", ""));
                    iTextSharp.text.Image qrImg = iTextSharp.text.Image.GetInstance(qrBytes);
                    qrImg.ScaleToFit(100f, 100f);
                    document.Add(qrImg);
                }

                document.Close();

                return File(ms.ToArray(), "application/pdf", "GS1_Label.pdf");
            }
        }
        /// <summary>
        /// Generates the barcode. Further informatin look for the ZXing library
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private string GenerateBarcode(string content)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,
                Options = new ZXing.Common.EncodingOptions
                {
                    Height = 100,
                    Width = 400,
                    Margin = 10
                }
            };

            using (var bitmap = writer.Write(content))
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        private string GenerateQrBase64(string content)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q))
            using (QRCode qrCode = new QRCode(qrCodeData))
            using (Bitmap qrBitmap = qrCode.GetGraphic(5))
            using (MemoryStream ms = new MemoryStream())
            {
                qrBitmap.Save(ms, ImageFormat.Png);
                var base64 = Convert.ToBase64String(ms.ToArray());
                return "data:image/png;base64," + base64;
            }
        }

    }
}