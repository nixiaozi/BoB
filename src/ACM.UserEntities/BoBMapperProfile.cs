using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.UserEntities
{
    public class BoBMapperProfile : Profile
    {
        public BoBMapperProfile()
        {
            CreateMap<Users, UserInput>(); 
            CreateMap<UserInput, Users>();
        }

    }
}
