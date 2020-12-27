using ACM.AppAccountListEntities;
using ACM.AppListEntities;
using ACM.BaseAutoAction;
using ACM.SinaChina;
using ACM.UserEntities;
using BoB.ExtendAndHelper.Utilties;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BoB.HelloWorldApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ACMController : ControllerBase
    {
        private IUserBlock _userBlock;
        private IAppListBlock _appListBlock;
        private IAppAccountListBlock _appAccountListBlock;
        private ISinaChinaWebService _sinaChinaWebService;
        private IEnumerable<AutoActionAdapter> _autoActionAdapters;


        public ACMController(IUserBlock userBlock, IAppListBlock appListBlock, IAppAccountListBlock appAccountListBlock,
            ISinaChinaWebService sinaChinaWebService, IEnumerable<AutoActionAdapter> autoActionAdapters)
        {
            _userBlock = userBlock;
            _appListBlock = appListBlock;
            _appAccountListBlock = appAccountListBlock;
            _sinaChinaWebService = sinaChinaWebService;
            _autoActionAdapters = autoActionAdapters;
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

        [HttpPost]
        public ActionResult<string> Encrypt(string EncryptText,string Salt)
        {
            var result = SecurityHelper.EncryptToBase64(EncryptText.Trim(),  Salt.Trim());

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<string> Decrypt(string DecryptText,  string Salt)
        {
            var result = SecurityHelper.DecryptFromBase64(DecryptText.Trim(),  Salt.Trim());

            return Ok(result);
        }


        [HttpPost]
        public ActionResult<string> AddAppAcount(AppAccountInput account)
        {
            var result = _appAccountListBlock.AddAppAccount(account);
            return result ? Ok("添加应用账户成功") : Problem("添加应用账户失败");
        }

        [HttpPost]
        public ActionResult<string> CookieUpdateTest(Guid  userId)
        {
            Cookie cookie = new Cookie("test", "abcd");
           var result= _appAccountListBlock.UpdateTheAccountCookie(userId, JsonConvert.SerializeObject(cookie));
            return result ? Ok("修改Cookie成功") : Problem("修改Cookie失败");
        }

        [HttpPost]
        public  ActionResult<string> SinaChinaLogin(Guid UserID)
        {
            var user =  _appAccountListBlock.GetAccountByUser(UserID);
            _sinaChinaWebService.ToLogin(user);

            return Ok();
        }

        [HttpPost]
        public ActionResult<string> AutofacAdapterTest()
        {
            _autoActionAdapters.FirstOrDefault(s => s.CommandText == "sinachina")?.DoBrowserRandom(new RandomBrowse());
            return Ok();
        }

    }
}
