using ACM.MainDatabase;
using BoB.AutoMapperManager;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

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
            return Delete(appID);
        }

        public List<AppList> GetAllApps(MaindbContext context)
        {
            return GetList(context,s => s.ID >= 0).ToList();
        }
    }
}
