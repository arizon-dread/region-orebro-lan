using clientside.backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using viewmodels;

namespace clientside.backend.Controllers
{
    public class SyncController(InfoService infoService, OrderService orderService) : BaseController
    {
        [HttpGet("changed/{date}")]
        public SyncViewModel GetUpdatedSince(DateTime date)
        {
            var data = new SyncViewModel {RequestDate = date };
            data.Info = infoService.UpdatedSince(date).ToList();
            data.Order = [];
            return data;
        }

    }
}
