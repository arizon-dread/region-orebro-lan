using clientside.backend.Service;
using Microsoft.AspNetCore.Mvc;
using viewmodels;

namespace clientside.backend.Controllers
{
    public class InfoController : BaseController
    {
        private readonly InfoService _infoService;
        public InfoController(InfoService itemService) 
        { 
            _infoService = itemService;
        }
        [HttpGet]
        public IEnumerable<Info> GetActive()
        {
            return _infoService.Active();
        }

        [HttpGet("all")]
        public IEnumerable<Info> GetAll()
        {
            return _infoService.All();
        }

        [HttpPost]
        public IActionResult SaveInfo(viewmodels.Info info)
        {
            if (info == null)
            {
                return BadRequest();
            }
            var response = _infoService.Save(info);
            return HandleResponseWrapperAndReturnResponseData(response);
        }
        

    }
}
