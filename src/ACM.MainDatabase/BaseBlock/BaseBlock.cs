using BoB.ContainManager;
using BoB.EFDbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.MainDatabase
{
    public class BaseBlock
    {
        protected readonly IServiceProvider CurrentServiceProvider;

        public BaseBlock()
        {
            CurrentServiceProvider = BoBContainer.ServiceProvider;

            Init();

            //这里可以在服务解析之后进行的基类处理
        }


        protected virtual void Init()
        {

        }


    }


    public class BaseBlock<T,K>:IBaseBlock<T,K> where T:IBaseEntity<K>
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
            throw new NotImplementedException();
        }

        public bool Get(K id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(T data)
        {
            throw new NotImplementedException();
        }

        public bool Update(T data, Func<T, T> func)
        {
            throw new NotImplementedException();
        }

        protected virtual void Init()
        {

        }


         



    }


}
