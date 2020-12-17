using ACM.UserEntities;
using Microsoft.AspNetCore.Mvc;


namespace BoB.HelloWorldApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ACMController : ControllerBase
    {
        private IUserBlock _userBlock;


        public ACMController(IUserBlock userBlock)
        {
            _userBlock = userBlock;
        }

        [HttpGet]
        public ActionResult<string> Hello()
        {
            return Ok("你好,欢迎！");
        }


        [HttpPost]
        public ActionResult<string> AddUser(UserInput userInput)
        {
            bool result = _userBlock.AddUser(userInput);
            return result ? Ok("添加用户成功") : Problem("添加用户失败");
        }


    }
}
