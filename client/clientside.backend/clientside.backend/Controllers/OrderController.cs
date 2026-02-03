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
        [HttpGet]
        public IActionResult Get()
        {

            return Ok();
        }
    }
}
