using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gs1BarcodeApplication.Models;
using Newtonsoft.Json;
using gs1BarcodeApplication.Helpers;
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

                string gs1Data = Gs1Builder.BuildGs1Data(model);

                return Json(new
                {
                    success = true,
                    gs1 = gs1Data
                });
            }
        }

    }
}