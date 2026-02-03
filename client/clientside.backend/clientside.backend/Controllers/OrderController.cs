using clientside.backend.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clientside.backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService) {
            _orderService = orderService;
        }
        [HttpGet]
        public IActionResult Get()
        {

            return Ok();
        }
    }
}
