using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.AppAccountListEntities
{
    public interface IAppAccountListBlock
    {
        public bool AddAppAccount(AppAccountInput account);

        public bool DeleteAccount(Guid accountId);

        public AppAccountList GetAccountByUser(Guid userId);

    }
}
