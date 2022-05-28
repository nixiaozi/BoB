using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.AppAccountListEntities
{
    public class BoBMapperProfile:Profile
    {
        public BoBMapperProfile()
        {
            CreateMap<AppAccountList, AppAccountInput>();
            CreateMap<AppAccountInput, AppAccountList>();
        }
    }
}
