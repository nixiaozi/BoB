using ACM.AppListEntities;
using ACM.UserEntities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BoB.HelloWorldApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ACMController : ControllerBase
    {
        private IUserBlock _userBlock;
        private IAppListBlock _appListBlock;


        public ACMController(IUserBlock userBlock, IAppListBlock appListBlock)
        {
            _userBlock = userBlock;
            _appListBlock = appListBlock;
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

        [HttpPost]
        public ActionResult<string> RemoveUser(Guid userId)
        {
            bool result = _userBlock.RemoveUser(userId);
            return result ? Ok("删除用户成功") : Problem("删除用户失败");
        }

        [HttpPost]
        public ActionResult<string> AddApp(AppInput appInput)
        {
            bool result = _appListBlock.AddApp(appInput);
            return result ? Ok("添加应用标识成功") : Problem("添加应用标识失败");
        }

        [HttpPost]
        public ActionResult<string> DeleteApp(int appID)
        {
            bool result = _appListBlock.DeleteApp(appID);
            return result ? Ok("删除应用标识成功") : Problem("删除应用标识失败");
        }


    }
}
