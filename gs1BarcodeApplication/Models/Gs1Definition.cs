using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace gs1BarcodeApplication.Models
{
    public class Gs1Definition
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("tooltip")]
        public string Tooltip { get; set; }

        [JsonProperty("validationRegex")]
        public string ValidationRegex { get; set; }
    }

    public class Gs1DefinitionList
    {
        [JsonProperty("gs1definitions")]
        public List<Gs1Definition> Definitions { get; set; }
    }
}