using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.AutoMapperManager
{
    public interface IAutoMapperService
    {
        public T DoMap<S,T>(S s) 
            where T:new() 
            where S:new();

        public T DoInsMap<S, T>(S s,T t)
            where T : new()
            where S : new();

    }
}
