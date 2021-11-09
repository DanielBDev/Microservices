using Microsoft.AspNetCore.Mvc;

namespace Customer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return "Running ..";
        }
    }
}
