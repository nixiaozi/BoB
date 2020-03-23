using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.BoBConfigManager
{
    /// <summary>
    /// 管理系统所有配置的方法
    /// </summary>
    public interface IBoBConfigService
    {
        public void DynamicConfigInit();
    }
}
