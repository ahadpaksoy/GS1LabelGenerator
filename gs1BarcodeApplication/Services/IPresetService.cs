using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gs1BarcodeApplication.Services
{
    public interface IPresetService
    {
        Dictionary<string, List<string>> GetDefaultPresets();
    }
}
