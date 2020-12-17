﻿using BoB.EFDbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.MainDatabase
{
    public interface IBaseBlock<T,K> where T:IBaseEntity<K>
    {
        public bool Insert(T data);


        public bool Update(T data,Func<T,T> func);


        public bool Delete(K id);

        public bool Get(K id);

    }
}