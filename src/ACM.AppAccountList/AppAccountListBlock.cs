using ACM.MainDatabase;
using BoB.AutoMapperManager;
using System;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Geometries;
using PasswordGenerator;
using BoB.EFDbContext.Enums;

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
            return Delete(accountId);
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

    }
}
