using clientside.backend.Service;
using Microsoft.AspNetCore.Mvc;
using viewmodels;

namespace clientside.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly InfoService _itemService;
        public InfoController(InfoService itemService) 
        { 
            _itemService = itemService;
        }
        [HttpGet]
        public IEnumerable<Info> Get()
        {
            return _itemService.Active();
        }
        
        [HttpPost]
        public ActionResult SaveInfo(Info info)
        {
            if (info == null)
            {
                return BadRequest();
            }
            _itemService.Save(info);
            return Ok();
        }

    }
}
