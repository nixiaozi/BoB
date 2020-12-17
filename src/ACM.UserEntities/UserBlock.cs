using BoB.AutoMapperManager;
using System;
using Microsoft.Extensions.DependencyInjection; // 解决CurrentServiceProvider.GetService<IAutoMapperService>();  出错的问题
using ACM.MainDatabase;
using System.Linq;
using BoB.EFDbContext.Enums;
using System.Diagnostics.CodeAnalysis;

namespace ACM.UserEntities
{
    public class UserBlock : BaseBlock<Users,Guid>,IBaseBlock<Users, Guid>, IUserBlock
    {
        private IAutoMapperService _autoMapperService;

        protected override void Init()
        {
            _autoMapperService = CurrentServiceProvider.GetService<IAutoMapperService>();
        }

        public Users GetUser(string Phone)
        {
            using (var context=new MaindbContext())
            {
                return context.Set<Users>().FirstOrDefault(s => s.Phone==Phone.Trim());
            }

        }

        public bool AddUser(UserInput user)
        {
            if (user.ID == Guid.Empty)
                user.ID = Guid.NewGuid();
            var hasUser = GetUser(user.Phone);

            if (hasUser == null)
                return Insert(user);
            else
            {
                return false;
            }

        }

        public bool RemoveUser(Guid userId)
        {
            return Delete(userId);

        }


        /*public bool Update(Users users,Func<Users,Users> func)
        {
            using(var context = new MaindbContext())
            {
                var data = context.Set<Users>().FirstOrDefault(s => s.ID == users.ID);
                func?.Invoke(data);

                context.SaveChanges();

                return true;
            }
        }
*/

    }
}
