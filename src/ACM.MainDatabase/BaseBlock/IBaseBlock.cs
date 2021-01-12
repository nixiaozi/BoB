using BoB.EFDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ACM.MainDatabase
{
    public interface IBaseBlock<T,K> where T:IBaseEntity<K> where K: IComparable
    {
        public bool Insert(T data);
        public bool Insert(MaindbContext context, T data);


        public bool Update(T data,Func<T,T> func);
        public bool Update(MaindbContext context, T data, Func<T, T> func);


        public bool Update(K id,Func<T,T> func);

        public bool Update(MaindbContext context, K id, Func<T, T> func);

        public bool Delete(K id);

        public bool Delete(MaindbContext context, K id);

        public bool Remove(K id);

        public bool Remove(MaindbContext context, K id);

        public T Get(K id);

        public T Get(MaindbContext context, K id);

        public T Get(Expression<Func<T, bool>> expression);

        public T Get(MaindbContext context, Expression<Func<T, bool>> expression);

        // public IQueryable<T> GetList(Expression<Func<T, bool>> expression);

        public IQueryable<T> GetList(MaindbContext context, Expression<Func<T, bool>> expression);

        public List<T> AsyncGetList(Expression<Func<T, bool>> expression);

    }
}
