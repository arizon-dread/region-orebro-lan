using clientside.backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace clientside.backend.Controllers
{
    public class ItemController : BaseController
    {
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var response = _itemService.GetAll();
            return HandleResponseWrapperAndReturnResponseData(response);
        }
        [HttpPost]
        public IActionResult SaveItem(viewmodels.Item item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var response = _itemService.Save(item);
            return HandleResponseWrapperAndReturnResponseData(response);
        }
        [HttpGet("{id}")]
        public IActionResult GetItem(Guid id)
        {
            var response = _itemService.Get(id);
            return HandleResponseWrapperAndReturnResponseData(response);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(Guid id)
        {
            var response = _itemService.Delete(id);
            return HandleResponseWrapperAndReturnResponseData(response);
        }
        [HttpPost("toggleactivation")]
        public IActionResult ToggleActivation(Guid id)
        {
            var response = _itemService.ToggleActivation(id);
            return HandleResponseWrapperAndReturnResponseData(response);
        }
    }
}
