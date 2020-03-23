using AutoMapper;
using BoB.AutoMapperManager.Test;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.AutoMapperManager
{
    public class BoBMapperProfile:Profile
    {
        public BoBMapperProfile()
        {
            CreateMap<Foo, FooDto>();
        }
            
    }
}
