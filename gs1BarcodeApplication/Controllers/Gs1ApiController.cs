// Controllers/Gs1ApiController.cs

using gs1BarcodeApplication.Services;
using System.Web.Http;

namespace gs1BarcodeApplication.Controllers
{
    
    [RoutePrefix("api/gs1")]
    public class Gs1ApiController : ApiController
    {
        private readonly IGs1DefinitionService _definitionService;

        
        public Gs1ApiController(IGs1DefinitionService definitionService)
        {
            _definitionService = definitionService;
        }

        [HttpGet]
        [Route("definitions")] 
        public IHttpActionResult GetDefinitions()
        {
            var definitions = _definitionService.GetAll();
            return Ok(definitions);
        }
    }
}