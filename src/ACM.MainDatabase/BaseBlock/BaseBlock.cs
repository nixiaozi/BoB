using BoB.ContainManager;
using BoB.EFDbContext;
using BoB.EFDbContext.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACM.MainDatabase
{
    public class BaseBlock<T,K>:IBaseBlock<T,K> where T:class,IBaseEntity<K>,new() where K : IComparable
    {
        protected readonly IServiceProvider CurrentServiceProvider;

        public BaseBlock()
        {
            CurrentServiceProvider = BoBContainer.ServiceProvider;

            Init();

            //这里可以在服务解析之后进行的基类处理
        }

        public bool Delete(K id)
        {
            T data = new T();
            data.ID = id;
            return Update(data, s =>
            {
                s.Status = DataStatus.Delete;
                return s;
            });
        }

        public T Get(K id)
        {
            using (var context = new MaindbContext())
            {
                return context.Set<T>().FirstOrDefault(s => s.ID.Equals(id)); // 直接使用 Equals 方法代替 == 操作符
            }
        }

        public bool Insert(T data)
        {
            using(var context=new MaindbContext())
            {
                context.Add<T>(data);
                context.SaveChanges();

                return true;
            }
        }

        public bool Update(T data, Func<T, T> func)
        {
            using (var context = new MaindbContext())
            {
                var getData = context.Set<T>().FirstOrDefault(s => s.ID.Equals(data.ID));
                func?.Invoke(getData);

                context.SaveChanges();

                return true;
            }
        }

        protected virtual void Init()
        {

        }


         



    }


}
