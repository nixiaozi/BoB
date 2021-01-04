using ACM.MainDatabase;
using BoB.AutoMapperManager;
using System;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Geometries;
using PasswordGenerator;
using BoB.EFDbContext.Enums;
using System.Collections.Generic;
using System.Linq;
using BoB.ExtendAndHelper.Utilties;

namespace ACM.AppAccountListEntities
{
    public class AppAccountListBlock: BaseBlock<AppAccountList, Guid>, IBaseBlock<AppAccountList, Guid>, IAppAccountListBlock
    {
        private IAutoMapperService _autoMapperService;

        protected override void Init()
        {
            _autoMapperService = CurrentServiceProvider.GetService<IAutoMapperService>();
        }


        public bool AddAppAccount(AppAccountInput account)
        {
            Random random = new Random();
            // 需要对输入数据进行处理
            account.Longitude = random.NextDouble() * 360 - 180; // 添加随机经度坐标
            account.Latitude = random.NextDouble() * 180 - 90;   // 添加随机纬度坐标
            account.Salt = new Password(4).IncludeNumeric().LengthRequired(4).Next(); // 定义随机的四位salt
            account.Password = SecurityHelper.EncryptToBase64(account.Password, account.Salt); //把传入的密码进行加密


            AppAccountList input = _autoMapperService.DoMap<AppAccountInput, AppAccountList>(account);
            input.ID = Guid.NewGuid();
            input.Location = new Point(account.Longitude, account.Latitude) { SRID = 4326,  };
            input.CreateTime = DateTime.Now;
            // input.Password = new Password(10).IncludeLowercase().IncludeNumeric().IncludeUppercase().IncludeSpecial().Next();
            // 这个方法可以直接获取
            return Insert(input);
        }

        public bool DeleteAccount(Guid accountId)
        {
            return Delete(new  MaindbContext(), accountId);
        }

        public bool UpdateAccount(AppAccountList input)
        {
            return Update(new MaindbContext(), input, s =>
            {
                s.Address = input.Address;
                //if (string.IsNullOrWhiteSpace(input.AppUserID)) s.AppUserID = input.AppUserID;
                //if (string.IsNullOrWhiteSpace(input.Cookie)) s.Cookie = input.Cookie;
                if (!string.IsNullOrWhiteSpace(input.Password))
                {
                    s.Password = SecurityHelper.EncryptToBase64(input.Password.Trim(), s.Salt);
                }
                s.NickName = input.NickName;
                return s;
            });
        }

        public AppAccountList GetAccountByUser(Guid userId)
        {
            return Get(s => s.UserID == userId);
        }

        public bool UpdateTheAccountCookie(Guid UserId, string Cookie)
        {
            // 无法把Func<A,A> 转化成 Func<B,B> 因为我们没有办法得到保证前一函数中的某些属性转到后一个函数后还有效
            //Func<AppAccountInput, Func<AppAccountInput, AppAccountInput>, AppAccountList> dofunc
            //    = delegate (AppAccountInput a, Func<AppAccountInput, AppAccountInput> b)
            //     {
            //         var ra = _autoMapperService.DoMap<AppAccountInput, AppAccountList>(a);
            //         b.Invoke(ra);

            //     };

            return Update(Get(s=>s.UserID==UserId && s.Status== DataStatus.Normal).ID, s=> {
                s.Cookie = Cookie;
                return s;
            });
        }

        public List<int> GetTheUserApps(Guid userID)
        {
            return GetList(new MaindbContext(), s => s.Status == DataStatus.Normal && s.UserID == userID).Select(s => s.AppID).ToList();

        }

        public List<AppAccountList> GetTheUserAccounts(Guid userID)
        {
            return GetList(new MaindbContext(), s => s.Status == DataStatus.Normal && s.UserID == userID).ToList();
        }

        

    }
}
