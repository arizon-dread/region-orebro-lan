using clientside.backend.Classes;
using clientside.backend.Enums;
using Microsoft.AspNetCore.Mvc;

namespace clientside.backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult HandleResponseWrapperAndReturnResponseData<T>(ServiceResponse<T> response, Exception? ex = null)
        {
            if (response == null && ex != null)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
            return response.Status switch
            {
                ServiceResponseEnum.Success => Ok(response.Data),
                ServiceResponseEnum.NotFound => StatusCode(404, response.Message),
                ServiceResponseEnum.Conflict => StatusCode(409, response.Message),
                ServiceResponseEnum.Deleted => StatusCode(204, response.Message),
                _ => StatusCode(500, response.Message),
            };
        }
    }


}
