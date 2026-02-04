using clientside.backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace clientside.backend.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var response = _customerService.GetAll();
            return HandleResponseWrapperAndReturnResponseData(response);
        }
        [HttpPost]
        public IActionResult SaveCustomer(viewmodels.Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            var response = _customerService.Save(customer);
            return HandleResponseWrapperAndReturnResponseData(response);
        }
        [HttpGet("{id}")]
        public IActionResult GetCustomer(Guid id)
        {
            var response = _customerService.Get(id);
            return HandleResponseWrapperAndReturnResponseData(response);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(Guid id)
        {
            var response = _customerService.Delete(id);
            return HandleResponseWrapperAndReturnResponseData(response);
        }
        [HttpPost("toggleactivation")]
        public IActionResult ToggleActivation(Guid id)
        {
            var response = _customerService.ToggleActivation(id);
            return HandleResponseWrapperAndReturnResponseData(response);
        }
    }
}
