using Microsoft.AspNetCore.Mvc;

namespace clientside.backend.Controllers
{
    public class HealthController: BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return Ok(DateTime.UtcNow);
        }
    }
}
