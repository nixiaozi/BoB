using BoB.AutoMapperManager;
using System;
using Microsoft.Extensions.DependencyInjection; // 解决CurrentServiceProvider.GetService<IAutoMapperService>();  出错的问题
using ACM.MainDatabase;
using System.Linq;
using BoB.EFDbContext.Enums;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using Autofac;

namespace ACM.UserEntities
{
    public class UserBlock : BaseBlock<Users,Guid>,IBaseBlock<Users, Guid>, IUserBlock
    {
        private IAutoMapperService _autoMapperService;

        protected override void Init()
        {
            _autoMapperService = CurrentServiceContainer.Resolve<IAutoMapperService>();
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
            if (String.IsNullOrEmpty(user.Phone.Trim()))
            {
                throw new Exception("用户手机号不能为空");
            }

            if (user.ID == Guid.Empty)
                user.ID = Guid.NewGuid();
            var hasUser = GetUser(user.Phone);

            if (hasUser == null)
            {
                user.CreateTime = DateTime.Now;
                return Insert(user);
            }
            else
            {
                return false;
            }

        }

        public bool RemoveUser(Guid userId)
        {
            return Delete(new MaindbContext(), userId);

        }

        public Users GetUserById(Guid userId)
        {
            return Get(userId);
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

        public List<Users> GetAllUserList(MaindbContext context = null)
        {
            if (context != null)
                return GetList(context, s => s.Status == DataStatus.Normal).ToList();
            else
                return GetList(new MaindbContext(), s => s.Status == DataStatus.Normal).ToList();
        }

        public bool UpdateUser(Users users)
        {
            return Update(new MaindbContext(), users, s =>
            {
                s.Brithday = users.Brithday;
                return s;
            });
        }


    }
}
