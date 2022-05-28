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

        public AppAccountList GetAccountByUser(Guid userId,int appID);

        public bool UpdateTheAccountCookie(Guid AccountID, string Cookie);

        public List<int> GetTheUserApps(Guid userID);

        public List<AppAccountList> GetTheUserAccounts(Guid userID);

        public List<SearchAccountOutput> SearchAccount(string SearchStr, int appID);

    }
}
