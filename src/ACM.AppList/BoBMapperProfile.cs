using AutoMapper;

namespace ACM.AppListEntities
{
    public class BoBMapperProfile : Profile
    {
        public BoBMapperProfile()
        {
            CreateMap<AppList, AppInput>(); 
            CreateMap<AppInput, AppList>();
        }

    }
}
