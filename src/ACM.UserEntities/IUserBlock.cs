using ACM.MainDatabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.UserEntities
{
     public interface IUserBlock// : IBaseBlock<Users,Guid>  不添加这个接口引用可以阻断前端对基础数据库操作方法的直接调用
    {
        public bool AddUser(UserInput user);

        public bool RemoveUser(Guid userId);

    }
}
