using ACM.MainDatabase;
using BoB.AutoMapperManager;
using System;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Geometries;
using PasswordGenerator;

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

    }
}
