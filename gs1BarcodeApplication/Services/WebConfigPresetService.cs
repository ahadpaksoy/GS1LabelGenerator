using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace gs1BarcodeApplication.Services
{
    public class WebConfigPresetService : IPresetService
    {
        public Dictionary<string, List<string>> GetDefaultPresets()
        {
            var presets = new Dictionary<string, List<string>>();

            foreach (var key in WebConfigurationManager.AppSettings.AllKeys)
            {
                if (key.StartsWith("preset:"))
                {
                    var presetName = key.Substring("preset:".Length);
                    var fieldString = WebConfigurationManager.AppSettings[key];
                    var fieldList = fieldString.Split(',')
                                               .Select(f => f.Trim())
                                               .ToList();
                    presets.Add(presetName, fieldList);
                }
            }
            return presets;
        }
    }
}