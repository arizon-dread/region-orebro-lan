using clientside.backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using viewmodels;

namespace clientside.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController(SettingsService settingsService) : BaseController
    {
        [HttpGet]
        public IEnumerable<ApplicationSettingsViewModel> Get()
        {
            return settingsService.GetSettings();
        }
        [HttpPost]
        public ApplicationSettingsViewModel Save(ApplicationSettingsViewModel model)
        {
            return settingsService.Save(model);
        }
    }
}
