// IGs1DefinitionService.cs

using gs1BarcodeApplication.Models;
using System.Collections.Generic;

namespace gs1BarcodeApplication.Services
{
    public interface IGs1DefinitionService
    {
        
        IEnumerable<Gs1Definition> GetAll();
        Gs1Definition GetByValue(string value);
    }
}