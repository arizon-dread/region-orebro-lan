using clientside.backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace clientside.backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _service;

        public OrderController(OrderService orderService) {
            _service = orderService;
        }
        [HttpGet("/get")]
        public IActionResult Get()
        {

            return Ok();
        }

        [HttpGet("/ready")]
        public IActionResult Ready()
        {
            return new JsonResult("Ready");
        }
    }
}
