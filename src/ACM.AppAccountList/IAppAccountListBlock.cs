using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.AppAccountListEntities
{
    public interface IAppAccountListBlock
    {
        public bool AddAppAccount(AppAccountInput account);

        public bool DeleteAccount(Guid accountId);

        public bool UpdateAccount(AppAccountList input);

        public AppAccountList GetAccountByUser(Guid userId);

        public bool UpdateTheAccountCookie(Guid UserId, string Cookie);

        public List<int> GetTheUserApps(Guid userID);

        public List<AppAccountList> GetTheUserAccounts(Guid userID);

    }
}
