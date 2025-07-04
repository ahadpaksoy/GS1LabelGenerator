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
            string gs1Data = BuildGs1Data(model);
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



        private string BuildGs1Data(ProductLabelModel m)
        {
            var parts = new List<string>();
            if (!string.IsNullOrEmpty(m.GTIN)) parts.Add($"(01){m.GTIN}");
            if (!string.IsNullOrEmpty(m.batch_lotNumber)) parts.Add($"(10){m.batch_lotNumber}");
            if (!string.IsNullOrEmpty(m.productionDate)) parts.Add($"(11){m.productionDate}");
            if (!string.IsNullOrEmpty(m.bestBeforeDate)) parts.Add($"(15){m.bestBeforeDate}");
            if (!string.IsNullOrEmpty(m.expirationDate)) parts.Add($"(17){m.expirationDate}");
            if (!string.IsNullOrEmpty(m.serialNumber)) parts.Add($"(21){m.serialNumber}");
            if (!string.IsNullOrEmpty(m.veriableCount)) parts.Add($"(30){m.veriableCount}");
            if (!string.IsNullOrEmpty(m.netWeight)) parts.Add($"(3102){m.netWeight}");
            if (!string.IsNullOrEmpty(m.netVolume)) parts.Add($"(3152){m.netVolume}");
            if (!string.IsNullOrEmpty(m.width)) parts.Add($"(3252){m.width}");
            if (!string.IsNullOrEmpty(m.netWeightUS)) parts.Add($"(3562){m.netWeightUS}");
            if (!string.IsNullOrEmpty(m.netVolumeUS)) parts.Add($"(3602){m.netVolumeUS}");
            if (!string.IsNullOrEmpty(m.amaountPayable)) parts.Add($"(3902){m.amaountPayable}");
            if (!string.IsNullOrEmpty(m.customerPONumber)) parts.Add($"(400){m.customerPONumber}");

            return string.Join("", parts);
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