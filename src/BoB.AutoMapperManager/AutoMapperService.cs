using AutoMapper;
using BoB.BoBConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BoB.AutoMapperManager
{
    public class AutoMapperService:IAutoMapperService
    {
        public static readonly MapperConfiguration configuration;
        public static readonly Mapper mapper;
        static AutoMapperService()
        {
            var NotProjectNamesList = StaticConfiguration.NotProjectNameArray().ToList();
            //扫描所有加载的程序集，获取所有添加的映射
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies()
                //.Where(s => s.FullName.Contains(StaticConfiguration.ProjectName) || s.FullName.Contains("BoB.HelloWorldApi")).ToArray();
                .Where(s =>
                {
                    bool flag = true;
                    int i = 0;
                    while(flag&&i< NotProjectNamesList.Count)
                    {
                        flag = !s.FullName.StartsWith(NotProjectNamesList[i]);
                        i++;
                    }
                    return flag;
                }).ToArray();

            configuration = new MapperConfiguration(cfg => {
                // 添加全局配置—— 不要映射所有为null的属性
                cfg.ForAllMaps((typeMap, map) =>
                    map.ForAllMembers(option => 
                        option.Condition((source, destination, sourceMember) => sourceMember != null)));
                foreach (var assembly in assemblies)
                {
                    cfg.AddMaps(assembly);
                }
            });

            mapper = new Mapper(configuration);
        }

        public T DoMap<S, T>(S s)
            where T : new()
            where S : new()
        {
            return mapper.Map<T>(s);
        }

        public T DoInsMap<S, T>(S s, T t)
            where S : new()
            where T : new()
        {
            return mapper.Map<S,T>(s,t);
        }
    }
}
