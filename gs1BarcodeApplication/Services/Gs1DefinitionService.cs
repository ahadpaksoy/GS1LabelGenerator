// Gs1DefinitionService.cs

using gs1BarcodeApplication.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace gs1BarcodeApplication.Services
{
   
    public class Gs1DefinitionService : IGs1DefinitionService
    {
        private static List<Gs1Definition> _definitionsCache;
        private static readonly object _lock = new object();

        public Gs1DefinitionService()
        {
            lock (_lock)
            {
                if (_definitionsCache == null)
                {
                    var filePath = HostingEnvironment.MapPath("~/gs1definitions.json");
                    var jsonContent = File.ReadAllText(filePath);
                    var definitionList = JsonConvert.DeserializeObject<Gs1DefinitionList>(jsonContent);
                    _definitionsCache = definitionList.Definitions;
                }
            }
        }

        public IEnumerable<Gs1Definition> GetAll()
        {
            return _definitionsCache;
        }

        public Gs1Definition GetByValue(string value)
        {
            return _definitionsCache.FirstOrDefault(d => d.Value == value);
        }
    }
}