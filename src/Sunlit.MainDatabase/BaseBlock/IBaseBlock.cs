using BoB.EFDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Sunlit.MainDatabase
{
    public interface IBaseBlock<T,K> where T:IBaseEntity<K> where K: IComparable
    {
        public bool Insert(T data);
        public bool Insert(SunlitMainContext context, T data);


        public bool Update(T data,Func<T,T> func);
        public bool Update(SunlitMainContext context, T data, Func<T, T> func);


        public bool Update(K id,Func<T,T> func);

        public bool Update(SunlitMainContext context, K id, Func<T, T> func);

        public bool Delete(K id);

        public bool Delete(SunlitMainContext context, K id);

        public bool Remove(K id);

        public bool Remove(SunlitMainContext context, K id);

        public T Get(K id);

        public T Get(SunlitMainContext context, K id);

        public T Get(Expression<Func<T, bool>> expression);

        public T Get(SunlitMainContext context, Expression<Func<T, bool>> expression);

        // public IQueryable<T> GetList(Expression<Func<T, bool>> expression);

        public IQueryable<T> GetList(SunlitMainContext context, Expression<Func<T, bool>> expression);

        public List<T> AsyncGetList(Expression<Func<T, bool>> expression);

    }
}
