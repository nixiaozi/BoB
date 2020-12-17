using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.UserEntities
{
     public interface IUserBlock
    {
        public bool AddUser(UserInput user);

        public bool RemoveUser(Guid userId);

    }
}
