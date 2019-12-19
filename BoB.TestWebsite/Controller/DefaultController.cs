using Microsoft.AspNetCore.Mvc;

namespace BoB.TestWebsite.Controller
{
    //如果使用[Route("api/[controller]")] 则只有一个url
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {

        public DefaultController()
        {
            
        }
        
        [HttpGet]
        public void Index()
        {
            
        }

    }
}