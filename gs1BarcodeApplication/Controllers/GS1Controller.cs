using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gs1BarcodeApplication.Models;
using Newtonsoft.Json;

namespace gs1BarcodeApplication.Controllers
{
    public class GS1Controller : Controller
    {
        [HttpPost]
        public JsonResult Generate()
        {
            Request.InputStream.Position = 0;
            using (var reader = new StreamReader(Request.InputStream))
            {
                var body = reader.ReadToEnd();
                var model = JsonConvert.DeserializeObject<ProductLabelModel>(body);

                string gs1Data = BuildGs1Data(model);

                return Json(new
                {
                    success = true,
                    gs1 = gs1Data
                });
            }
        }

        /// <summary>
        /// creates a string list named part which contains gs1 label parts like gtin, serial number
        /// at end of the function it concatenates all the parts and returns as a string
        /// </summary>
        /// <param name="model"></param>
        /// <returns>String </returns>
        private string BuildGs1Data(ProductLabelModel model)
        {
          
            var parts = new List<string>();

            if (!string.IsNullOrWhiteSpace(model.GTIN))
                parts.Add("(01)" + model.GTIN);
            if (!string.IsNullOrWhiteSpace(model.batch_lotNumber))
                parts.Add("(10)" + model.batch_lotNumber);
            if (!string.IsNullOrWhiteSpace(model.productionDate))
                parts.Add("(11)" + model.productionDate);
            if (!string.IsNullOrWhiteSpace(model.bestBeforeDate))
                parts.Add("(15)" + model.bestBeforeDate);
            if (!string.IsNullOrWhiteSpace(model.expirationDate))
                parts.Add("(17)" + model.expirationDate);
            if (!string.IsNullOrWhiteSpace(model.serialNumber))
                parts.Add("(21)" + model.serialNumber);
            if (!string.IsNullOrWhiteSpace(model.veriableCount))
                parts.Add("(30)" + model.veriableCount);
            if (!string.IsNullOrWhiteSpace(model.netWeight))
                parts.Add("(3102)" + model.netWeight);
            if (!string.IsNullOrWhiteSpace(model.netVolume))
                parts.Add("(3152)" + model.netVolume);
            if (!string.IsNullOrWhiteSpace(model.width))
                parts.Add("(3252)" + model.width);
            if (!string.IsNullOrWhiteSpace(model.netWeightUS))
                parts.Add("(3562)" + model.netWeightUS);
            if (!string.IsNullOrWhiteSpace(model.netVolumeUS))
                parts.Add("(3602)" + model.netVolumeUS);
            if (!string.IsNullOrWhiteSpace(model.amaountPayable))
                parts.Add("(3902)" + model.amaountPayable);
            if (!string.IsNullOrWhiteSpace(model.customerPONumber))
                parts.Add("(400)" + model.customerPONumber);

            return string.Join("", parts);
        }
    }
}