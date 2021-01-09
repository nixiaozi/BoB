using ACM.MainDatabase;
using BoB.AutoMapperManager;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using BoB.EFDbContext.Enums;
using System.Threading.Tasks;

namespace ACM.AppListEntities
{
    public class AppListBlock : BaseBlock<AppList, int>, IBaseBlock<AppList, int>, IAppListBlock
    {
        private IAutoMapperService _autoMapperService;
        protected override void Init()
        {
            _autoMapperService = CurrentServiceProvider.GetService<IAutoMapperService>();
        }

        public bool AddApp(AppInput app)
        {
            return Insert(_autoMapperService.DoMap<AppInput,AppList>(app));
        }

        public bool DeleteApp(int appID)
        {
            return Delete(new MaindbContext(),appID);
        }

        public List<AppList> GetAllApps(MaindbContext context=null)
        {
            if (context != null)
                return GetList(context, s => s.ID >= 0).ToList();
            else
                return GetList(new MaindbContext(),s => s.Status == DataStatus.Normal).ToList();
        }

        public List<AppList> GetAllTheApps()
        {
            return AsyncGetList( s => s.ID >= 0).ToList();
        }

        public bool UpdateTheApp(AppInput newer)
        {
            return Update(new MaindbContext(), newer, s =>
            {
                s.AppName = newer.AppName;
                s.IdentityName = newer.IdentityName;
                s.WebDomain = newer.WebDomain;
                return s;
            });
        }

        public bool RemoveTheApp(int appID)
        {
            return Remove(new MaindbContext(), appID);
        }
    }
}
