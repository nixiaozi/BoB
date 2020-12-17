using BoB.AutoMapperManager;
using System;
using Microsoft.Extensions.DependencyInjection; // 解决CurrentServiceProvider.GetService<IAutoMapperService>();  出错的问题
using ACM.MainDatabase;
using System.Linq;
using BoB.EFDbContext.Enums;

namespace ACM.UserEntities
{
    public class UserBlock : BaseBlock, IUserBlock
    {
        private IAutoMapperService _autoMapperService;

        protected override void Init()
        {
            _autoMapperService = CurrentServiceProvider.GetService<IAutoMapperService>();
        }

        public bool AddUser(UserInput user)
        {
            if (user.ID == Guid.Empty)
                user.ID = Guid.NewGuid();

            using(var context = new MaindbContext())
            {
                context.Add<Users>(_autoMapperService.DoMap<UserInput, Users>(user));
                context.SaveChanges();
            }

            return true;

        }

        public bool RemoveUser(Guid userId)
        {
            Users user = new Users { ID = userId };
            return Update(user, s =>
             {
                 s.Status = DataStatus.Delete;
                 return s;
             });

        }


        public bool Update(Users users,Func<Users,Users> func)
        {
            using(var context = new MaindbContext())
            {
                var data = context.Set<Users>().FirstOrDefault(s => s.ID == users.ID);
                func?.Invoke(data);

                context.SaveChanges();

                return true;
            }
        }


    }
}
