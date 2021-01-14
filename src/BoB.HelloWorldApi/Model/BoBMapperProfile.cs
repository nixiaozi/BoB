using ACM.AppAccountListEntities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoB.HelloWorldApi.Model
{
    public class BoBMapperProfile:Profile
    {
        public BoBMapperProfile()
        {
            CreateMap<AppAccountList, GetTheUserAccountOutput>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.appID, opt => opt.MapFrom(src => src.AppID))
                .ForMember(dest => dest.createTime, opt => opt.MapFrom(src => src.CreateTime))
                .ForMember(dest => dest.lastUrl, opt => opt.MapFrom(src => src.LastURL))
                .ForMember(dest => dest.latitude, opt => opt.MapFrom(src => src.Location.Y))
                .ForMember(dest => dest.longitude, opt => opt.MapFrom(src => src.Location.X))
                .ForMember(dest => dest.nickName, opt => opt.MapFrom(src => src.NickName));

            CreateMap<SearchAccountOutput, OptionItem<Guid, string>>()
                .ForMember(dest => dest.key, opt => opt.MapFrom(s => s.UserID))
                .ForMember(dest => dest.value, opt => opt.MapFrom(s => s.NickName));
        }
    }
}
