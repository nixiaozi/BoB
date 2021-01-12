using Autofac;
using BoB.ContainManager;
using BoB.EFDbContext;
using BoB.EFDbContext.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ACM.MainDatabase
{
    public class BaseBlock<T,K>:IBaseBlock<T,K> where T:class,IBaseEntity<K>,new() where K : IComparable
    {
        protected readonly IContainer CurrentServiceContainer;

        public BaseBlock()
        {
            CurrentServiceContainer = BoBContainer.ServiceContainer;

            Init();

            //这里可以在服务解析之后进行的基类处理
        }

        public bool Remove(K id)
        {
            using (var context = new MaindbContext())
            {
                var item =context.Set<T>().FirstOrDefault(s => s.ID.Equals(id));
                context.Remove<T>(item);
                context.SaveChanges();
                return true;
            }
        }

        public bool Remove(MaindbContext context, K id)
        {
            var item = context.Set<T>().FirstOrDefault(s => s.ID.Equals(id));
            context.Remove<T>(item);
            context.SaveChanges();
            return true;
        }

        public bool Delete(MaindbContext context, K id)
        {
            T data = new T();
            data.ID = id;
            return Update(context,data, s =>
            {
                s.Status = DataStatus.Delete;
                return s;
            });
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



        public T Get(MaindbContext context, Expression<Func<T,bool>> expression)
        {
            return context.Set<T>().FirstOrDefault(expression);
        }
        public T Get(Expression<Func<T, bool>> expression)
        {
            using (var context = new MaindbContext())
            {
                return context.Set<T>().FirstOrDefault(expression);
            }
        }



        public IQueryable<T> GetList(MaindbContext context, Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().Where(expression).AsQueryable();
        }

        //public IQueryable<T> GetList(Expression<Func<T, bool>> expression)
        //{
        //    using (var context = new MaindbContext())
        //    {
        //        return context.Set<T>().Where(expression).AsQueryable();
        //    }
        //}


        public List<T> AsyncGetList(Expression<Func<T, bool>> expression)
        {
            using (var context = new MaindbContext())
            {
                return context.Set<T>().Where(expression).ToList();
            }
        }




        public T Get(MaindbContext context, K id)
        {
            return context.Set<T>().FirstOrDefault(s => s.ID.Equals(id)); // 直接使用 Equals 方法代替 == 操作符

        }
        public T Get(K id)
        {
            using (var context = new MaindbContext())
            {
                return context.Set<T>().FirstOrDefault(s => s.ID.Equals(id)); // 直接使用 Equals 方法代替 == 操作符
            }
        }



        public virtual bool Insert(MaindbContext context, T data)
        {
            context.Add<T>(data);
            context.SaveChanges();
            return true;
        }
        public virtual bool Insert(T data)
        {
            using (var context = new MaindbContext())
            {
                context.Add<T>(data);
                context.SaveChanges();

                return true;
            }
        }


        public bool Update(MaindbContext context, T data, Func<T, T> func)
        {
            return Update(context,data.ID, func);
        }
        public bool Update(T data, Func<T, T> func)
        {
            return Update(data.ID, func);
        }



        public bool Update(MaindbContext context,K id, Func<T, T> func)
        {
            var getData = context.Set<T>().FirstOrDefault(s => s.ID.Equals(id));
            func?.Invoke(getData);

            context.SaveChanges();
            return true;
        }
        public bool Update(K id, Func<T, T> func)
        {
            using (var context = new MaindbContext())
            {
                var getData = context.Set<T>().FirstOrDefault(s => s.ID.Equals(id));
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
