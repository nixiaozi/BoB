using ACM.AllTasksEntities;
using ACM.AppAccountListEntities;
using ACM.AppListEntities;
using ACM.BaseAutoAction;
using ACM.EmailManager;
using ACM.SinaChina;
using ACM.TaskManager;
using ACM.TaskManager.Model;
using ACM.UserEntities;
using Autofac;
using BoB.AutoMapperManager;
using BoB.BoBContainManager;
using BoB.ExtendAndHelper.Extends;
using BoB.ExtendAndHelper.Utilties;
using BoB.HelloWorldApi.Model;
using BoB.PeopleEntities;
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
    public class ACMController : ACMBaseController
    {
        private IUserBlock _userBlock;
        private IAppListBlock _appListBlock;
        private IAppAccountListBlock _appAccountListBlock;
        private ISinaChinaWebService _sinaChinaWebService;
        private IEnumerable<AutoActionAdapter> _autoActionAdapters;
        private IAutoMapperService _autoMapperService;
        private IAllTasksBlock _allTasksBlock;
        private ITaskManagerService _taskManagerService;
        private IPeopleBlock _peopleBlock;
        private IEmailService _emailService;


        public ACMController()
        {
            _userBlock = BoBContainer.ServiceContainer.Resolve<IUserBlock>();
            _appListBlock = BoBContainer.ServiceContainer.Resolve<IAppListBlock>();
            _appAccountListBlock = BoBContainer.ServiceContainer.Resolve<IAppAccountListBlock>();
            _sinaChinaWebService = BoBContainer.ServiceContainer.Resolve<ISinaChinaWebService>();
            _autoActionAdapters = BoBContainer.ServiceContainer.Resolve<IEnumerable<AutoActionAdapter>>();
            _autoMapperService = BoBContainer.ServiceContainer.Resolve<IAutoMapperService>();
            _allTasksBlock = BoBContainer.ServiceContainer.Resolve<IAllTasksBlock>();
            _taskManagerService = BoBContainer.ServiceContainer.Resolve<ITaskManagerService>();
            _peopleBlock = BoBContainer.ServiceContainer.Resolve<IPeopleBlock>();
            _emailService = BoBContainer.ServiceContainer.Resolve<IEmailService>();
        }


        [HttpGet]
        public ActionResult<string> SendWarnEmail()
        {
            _emailService.ACMEmailAutoWarn("用户：测试发送警告邮件！" );
            return Ok("完成发送邮件！");
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
        public ActionResult<string> UpdateUser(UserInput users)
        {
            users.Phone = null;
            var oldUser = _userBlock.GetUserById(users.ID);
            var updateUser = _autoMapperService.DoInsMap<UserInput, Users>(users, oldUser);


            bool result = _userBlock.UpdateUser(updateUser);
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
        public ActionResult<string> CookieUpdateTest(Guid  accountID)
        {
            Cookie cookie = new Cookie("test", "abcd");
           var result= _appAccountListBlock.UpdateTheAccountCookie(accountID, JsonConvert.SerializeObject(cookie));
            return result ? Ok("修改Cookie成功") : Problem("修改Cookie失败");
        }

        [HttpPost]
        public  ActionResult<string> SinaChinaLogin(Guid UserID,int appID=4)
        {
            var user =  _appAccountListBlock.GetAccountByUser(UserID,appID);
            _sinaChinaWebService.ToLogin(user);

            return Ok();
        }

        [HttpPost]
        public ActionResult<string> AutofacAdapterTest()
        {
            _autoActionAdapters.FirstOrDefault(s => s.CommandText == "sinachina")?.DoBrowserRandom(Guid.NewGuid(),new RandomBrowse(),new System.Threading.CancellationToken());
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

        [HttpGet]
        public ActionResult<List<OptionItem<int,string>>> GetTaskTypes()
        {
            List<OptionItem<int, string>> data = new List<OptionItem<int, string>>();
            foreach (ACMTaskTypeEnum item in (ACMTaskTypeEnum[])Enum.GetValues(typeof(ACMTaskTypeEnum)))
            {
                var it = new OptionItem<int, string> { key = (int)item, value = item.DisplayName() };
                data.Add(it);
            }

            return Ok(data);
        }

        [HttpGet]
        public ActionResult<List<OptionItem<int, string>>> GetTaskLevels()
        {
            List<OptionItem<int, string>> data = new List<OptionItem<int, string>>();
            foreach (ACMTaskLevelEnum item in (ACMTaskLevelEnum[])Enum.GetValues(typeof(ACMTaskLevelEnum)))
            {
                var it = new OptionItem<int, string> { key = (int)item, value = item.DisplayName() };
                data.Add(it);
            }

            return Ok(data);
        }

        [HttpGet]
        public ActionResult<List<OptionItem<Guid, string>>> SearchAccount(string SearchStr,int appID)
        {
            var result = SearchStr==null?null: _appAccountListBlock.SearchAccount(SearchStr.Trim(), appID);
            return Ok(_autoMapperService.DoMap<List<SearchAccountOutput>, List<OptionItem<Guid, string>>>(result));

        }

        [HttpPost]
        public ActionResult<string> AddNewTask(AddNewTaskModel model)
        {
            AllTasks data = new AllTasks
            {
                AppID = model.appID,
                CreateTime = DateTime.Now,
                ParamObj = model.taskParamStr,
                Status = EFDbContext.Enums.DataStatus.Normal,
                TaskExecuteStatus = TaskExecuteStatusEnum.UnDo,
                TaskLevel = model.taskLevel,
                TaskType = model.taskType,
                UserID = model.userID,
            };
            var result = _allTasksBlock.AddNewTask(data);

            return result? Ok("添加任务成功") : Problem("添加任务失败");
        }

        [HttpGet]
        public ActionResult<string> RemoveTheApp(int appID)
        {
            var result = _appListBlock.RemoveTheApp(appID);
            return result ? Ok("删除应用成功") : Problem("删除应用失败");
        }

        [HttpGet]
        public ActionResult<List<TaskDetailOutput>> GetAllTask()
        {
            var result = _taskManagerService.GetUndoneTaskDetail();
            return Ok(result);
        }

    }
}
