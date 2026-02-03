using clientside.backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using viewmodels;

namespace clientside.backend.Controllers
{
    public class SyncController(InfoService infoService, OrderService orderService, ItemService itemService) : BaseController
    {
        [HttpGet("changed/{date}")]
        public SyncViewModel GetUpdatedSince(DateTime date)
        {
            var data = new SyncViewModel {RequestDate = date };
            data.Info = infoService.UpdatedSince(date).ToList();
            data.Order = orderService.UpdatedSince(date).ToList();
            data.Item = itemService.UpdatedSince(date).ToList();
            return data;
        }

    }
}
