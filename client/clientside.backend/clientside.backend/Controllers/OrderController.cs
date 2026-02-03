using clientside.backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace clientside.backend.Controllers
{
    public class OrderController : BaseController
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService) {
            _orderService = orderService;
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var response = _orderService.GetAll();
            return HandleResponseWrapperAndReturnResponseData(response);
        }
        [HttpPost]
        public IActionResult SaveOrder(viewmodels.Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            var response = _orderService.Save(order);
            return HandleResponseWrapperAndReturnResponseData(response);
        }
    }
}
