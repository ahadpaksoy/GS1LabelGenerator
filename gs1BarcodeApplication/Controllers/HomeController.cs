// HomeController.cs (Cleaned up)

using System;
using System.Collections.Generic;
using System.Linq;
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
using Newtonsoft.Json;
using System.Web.Configuration;


namespace gs1BarcodeApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var presets = new Dictionary<string, List<string>>();

            // Loop through all keys in appSettings
            foreach (var key in WebConfigurationManager.AppSettings.AllKeys)
            {
                // Check if the key starts with our prefix
                if (key.StartsWith("preset:"))
                {
                    // Get the preset name by removing the prefix
                    var presetName = key.Substring("preset:".Length);

                    // Get the comma-separated string of fields
                    var fieldsString = WebConfigurationManager.AppSettings[key];

                    // Split the string into a list and remove any extra whitespace
                    var fieldsList = fieldsString.Split(',')
                                                 .Select(f => f.Trim())
                                                 .ToList();

                    presets.Add(presetName, fieldsList);
                }
            }

            // Pass the presets dictionary to the view
            ViewBag.Presets = presets;

            return View();
        }

        // ... Submit, ExportPdf, and other methods remain unchanged ...
        [HttpPost]
        public ActionResult Submit(List<Gs1FieldInput> inputs)
        {
            if (inputs == null || inputs.Count == 0)
            {
                ModelState.AddModelError("", "No inputs received.");
                // Pass back the ViewBag data even on error
                var presetsFilePath = Server.MapPath("~/presets.json");
                var presets = new Dictionary<string, List<string>>();
                if (System.IO.File.Exists(presetsFilePath))
                {
                    var jsonContent = System.IO.File.ReadAllText(presetsFilePath);
                    presets = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonContent);
                }
                ViewBag.Presets = presets;
                return View("Index");
            }
            var model = new ProductLabelModel();

            foreach (var input in inputs)
            {
                var prop = typeof(ProductLabelModel).GetProperty(input.Field);
                if (prop != null && prop.CanWrite)
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