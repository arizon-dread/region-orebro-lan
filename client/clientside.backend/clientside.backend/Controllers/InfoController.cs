using clientside.backend.Service;
using Microsoft.AspNetCore.Mvc;
using viewmodels;

namespace clientside.backend.Controllers
{
    public class InfoController : BaseController
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
        public IActionResult SaveInfo(viewmodels.Info info)
        {
            if (info == null)
            {
                return BadRequest();
            }
            var response = _itemService.Save(info);
            return HandleResponseWrapperAndReturnResponseData(response);
        }

    }
}
