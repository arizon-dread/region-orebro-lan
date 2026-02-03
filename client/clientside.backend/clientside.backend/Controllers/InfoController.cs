using clientside.backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
        [HttpGet()]
        public ActionResult Getq()
        {
            return Ok();
        }
        
        [HttpPost]
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
