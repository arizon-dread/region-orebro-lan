using clientside.backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace clientside.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
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
        
        [HttpGet("{type}")]
        public ActionResult Get(string type)
        {
            return Ok(type);
        }
        [HttpGet("types")]
        public IEnumerable<string> Get()
        {
            return _types;
        }

        [HttpPost("/info")]
        public ActionResult SaveInfo(viewmodels.Info info)
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
