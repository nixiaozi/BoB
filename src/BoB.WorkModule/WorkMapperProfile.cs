using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.Work
{
    public class WorkMapperProfile:Profile
    {
        public WorkMapperProfile()
        {
            CreateMap<School, SchoolDto>();
        }
    }
}
