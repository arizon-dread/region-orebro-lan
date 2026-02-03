using clientside.backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace clientside.backend.Controllers
{
    public class ItemController : BaseController
    {
        private List<string> _types = new List<string> { "Info", "Order" };
        private readonly InfoService _itemService;
        public ItemController(InfoService itemService) 
        { 
            _itemService = itemService;
        }
        [HttpGet()]
        public ActionResult Getq()
        {
            return Ok();
        }
        
        [HttpGet("/{type}")]
        public ActionResult Get(string type)
        {
            return Ok(type);
        }
        [HttpGet("/types")]
        public IEnumerable<string> Get()
        {
            return _types;
        }

        [HttpPost("/info")]
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
