

using gs1BarcodeApplication.Helpers;
using gs1BarcodeApplication.Models;
using gs1BarcodeApplication.Services;
using QuestPDF.Fluent;
using gs1BarcodeApplication.Documents;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Mvc;
using ZXing;
using Serilog;
using Serilog.Formatting.Compact;

namespace gs1BarcodeApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPresetService _presetService;
        public HomeController(IPresetService presetService)
        {
            _presetService = presetService;
        }
        public ActionResult Index()
        {
            var presets = _presetService.GetDefaultPresets();
            ViewBag.Presets = presets;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Submit(List<Gs1FieldInput> inputs)
        {
            try
            {
                if (inputs == null || !inputs.Any())
                {
                    
                    return Json(new { success = false, message = "No input data received." });
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

                // Server-side validation check
                TryValidateModel(model);
                if (!ModelState.IsValid)
                {
                    var validationFailures = ModelState
                        .Where(ms => ms.Value.Errors.Any())
                        .Select(ms => new
                        {
                            Field = ms.Key, // The name of the property, e.g., "GTIN"
                                            // Get the invalid value that was submitted for this field.
                    AttemptedValue = ms.Value.Value?.AttemptedValue,
                            Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
                        })
                        .ToList();
                    Log.Warning("Submit failed due to model validation errors. Failures: {@ValidationFailures}", validationFailures);
                    // Return a JSON object with the aggregated error messages
                    var userErrorMessages = validationFailures.SelectMany(f => f.Errors);
                    return Json(new { success = false, message = string.Join("\n", userErrorMessages) });
                }

                // If validation passes, generate data
                string gs1Data = Gs1Builder.BuildGs1Data(model);
                string barcodeBase64 = GenerateBarcode(gs1Data);
                string qrImage = GenerateQrBase64(gs1Data);
                Log.Information("Successfully generated GS1 data for GTIN {GTIN}", model.GTIN);
                // Convert the populated model to a dictionary for the client-side summary table
                var modelData = model.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.GetValue(model, null) != null && !string.IsNullOrWhiteSpace(p.GetValue(model, null).ToString()))
                    .ToDictionary(p => p.Name, p => p.GetValue(model, null).ToString());

                // Return a JSON object on success
                return Json(new
                {
                    success = true,
                    gs1String = gs1Data,
                    barcodeImage = "data:image/png;base64," + barcodeBase64,
                    qrImage = qrImage,
                    modelData = modelData

                });
            }
            catch (Exception ex)
            {

                Log.Error(ex, "An unhandled exception occurred during submission.");
                return Json(new { success = false, message = "An unexpected error occurred. The administrator has been notified." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileResult ExportPdf(string gs1String, string barcodeBase64, string qrBase64)
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            byte[] barcodeBytes = !string.IsNullOrEmpty(barcodeBase64) ? Convert.FromBase64String(barcodeBase64) : null;
            byte[] qrCodeBytes = !string.IsNullOrEmpty(qrBase64) ? Convert.FromBase64String(qrBase64) : null;
            var document = new Gs1LabelDocument(gs1String, barcodeBytes, qrCodeBytes);
            byte[] pdfBytes = document.GeneratePdf();
            var fileName = $"GS1_Label_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            return File(pdfBytes, "application/pdf", fileName);
        }
        [HttpPost]
        public JsonResult LogClientValidationFailure(List<Gs1FieldInput> inputs)
        {
            Log.Warning("Client-side validation failed. User attempted to submit with invalid data: {@InvalidInputs}", inputs);
            return Json(new { success = true });
        }
        // --- Helper methods ---
        private string GenerateBarcode(string content)
        {
            if (string.IsNullOrEmpty(content)) return string.Empty;
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,
                Options = new ZXing.Common.EncodingOptions { Height = 100, Width = 400, Margin = 10 }
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
            if (string.IsNullOrEmpty(content)) return string.Empty;
            using (var qrGenerator = new QRCodeGenerator())
            using (var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q))
            using (var qrCode = new QRCode(qrCodeData))
            using (var qrBitmap = qrCode.GetGraphic(5))
            using (var ms = new MemoryStream())
            {
                qrBitmap.Save(ms, ImageFormat.Png);
                return "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}