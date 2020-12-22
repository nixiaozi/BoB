using ACM.AppAccountListEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.SinaChina
{
    public interface ISinaChinaWebService
    {
        public bool ToLogin(AppAccountList account);

    }
}
