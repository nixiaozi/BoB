using ACM.AppAccountListEntities;
using ACM.AppListEntities;
using ACM.BaseAutoAction;
using ACM.SinaChina;
using ACM.UserEntities;
using BoB.AutoMapperManager;
using BoB.ExtendAndHelper.Utilties;
using BoB.HelloWorldApi.Model;
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
        private IAutoMapperService _autoMapperService;


        public ACMController(IUserBlock userBlock, IAppListBlock appListBlock, IAppAccountListBlock appAccountListBlock,
            ISinaChinaWebService sinaChinaWebService, IEnumerable<AutoActionAdapter> autoActionAdapters,
            IAutoMapperService autoMapperService)
        {
            _userBlock = userBlock;
            _appListBlock = appListBlock;
            _appAccountListBlock = appAccountListBlock;
            _sinaChinaWebService = sinaChinaWebService;
            _autoActionAdapters = autoActionAdapters;
            _autoMapperService = autoMapperService;
        }

        [HttpGet]
        public ActionResult<string> Hello()
        {
            return Ok("你好,欢迎！");
        }


        [HttpPost]
        public ActionResult<string> AddUser(UserInput userInput)
        {
            if (String.IsNullOrWhiteSpace(userInput.Phone))
            {
                return Problem("用户手机不能为空");
            }

            bool result = _userBlock.AddUser(userInput);
            return result ? Ok("添加用户成功") : Problem("添加用户失败");
        }

        [HttpPost]
        public ActionResult<string> UpdateUser(Users users)
        {
            bool result = _userBlock.UpdateUser(users);
            return result ? Ok("修改用户信息成功") : Problem("修改用户信息失败");
        }


        [HttpPost]
        public ActionResult<string> DeleteUser(ACMDeleteUserModel data)
        {
            bool result = _userBlock.RemoveUser(data.userID);
            return result ? Ok("删除用户成功") : Problem("删除用户失败");
        }

        [HttpPost]
        public ActionResult<string> AddApp(AppInput appInput)
        {
            bool result = _appListBlock.AddApp(appInput);
            return result ? Ok("添加应用标识成功") : Problem("添加应用标识失败");
        }

        [HttpPost]
        public ActionResult<string> DeleteApp(ACMDeleteAppModel model)
        {
            bool result = _appListBlock.DeleteApp(model.appID);
            return result ? Ok("删除应用标识成功") : Problem("删除应用标识失败");
        }

        [HttpPost]
        public ActionResult<string> UpdateApp(AppInput newer)
        {
            bool result = _appListBlock.UpdateTheApp(newer);
            return result ? Ok("更新应用标识成功") : Problem("更新应用标识失败");
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
        public ActionResult<string> AddAppAcount(ACMAddAccountModel data)
        {
            var account = new AppAccountInput
            {
                Address = data.address,
                AppID = data.appID,
                AppUserID = "Undefined",
                NickName = data.nickName,
                Password = data.password,
                UserID=data.userID,
            };
            var result = _appAccountListBlock.AddAppAccount(account);
            return result ? Ok("添加应用账户成功") : Problem("添加应用账户失败");
        }

        [HttpPost]
        public ActionResult<string> UpdateAccount(ACMEditAccountModel data)
        {
            var account = new AppAccountList
            {
                ID = data.id,
                Address = data.address,
                NickName = data.nickName,
                Password = data.password,
            };
            var result = _appAccountListBlock.UpdateAccount(account);
            return result ? Ok("修改应用账户成功") : Problem("修改应用账户失败");
        }

        [HttpPost]
        public ActionResult<string> DeleteAccount(ACMDeleteAccountModel model)
        {
            var result = _appAccountListBlock.DeleteAccount(model.accountID);
            return result ? Ok("删除应用账户成功") : Problem("删除应用账户失败");
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

        [HttpGet]
        public ActionResult<List<AppList>> GetAllAppList()
        {
            var result = _appListBlock.GetAllApps();
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<Users>> GetAllUserList()
        {
            var result = _userBlock.GetAllUserList();
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<int>> GetTheUserApps(Guid userID)
        {
            var result = _appAccountListBlock.GetTheUserApps(userID);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<GetTheUserAccountOutput>> GetTheUserAccount(Guid userID)
        {

            var result = _appAccountListBlock.GetTheUserAccounts(userID);
            return Ok(_autoMapperService.DoMap<List<AppAccountList>,List<GetTheUserAccountOutput>>(result));
        }



    }
}
