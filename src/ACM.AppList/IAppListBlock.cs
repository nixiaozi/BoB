using ACM.MainDatabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.AppListEntities
{
    public interface IAppListBlock
    {
        public bool AddApp(AppInput app);

        public bool DeleteApp(int appID);

        public List<AppList> GetAllApps(MaindbContext context=null);

        public bool UpdateTheApp(AppInput newer);
    }
}
