using Autofac;
using Autofac.Configuration;
using BoB.AutoMapperManager;
using BoB.BoBLogger;
using BoB.CacheManager;
using BoB.ContainManager;
using BoB.EmailManager;
using BoB.LanguageManager;
using Microsoft.Extensions.Configuration;
using Sunlit.BussinessAssociaterEntities;
using System;
using System.IO;

namespace Sunlit.UseBus
{
    public static class BaseRegister
    {

        public static ContainerBuilder RegisterConfigureContainer(ContainerBuilder builder)
        {
            //模块注入,只有注入模块之后，程序集扫描才能获得该assembly
            //BoB.Base部分模块注入
            builder.RegisterModule<BoB.BaseModule.BoBModule>();
            builder.RegisterModule<BoBLoggerModule>();
            builder.RegisterModule<CacheManagerModule>();
            builder.RegisterModule<LanguageManagerModule>();
            builder.RegisterModule<EmailManagerModule>();

            //BoB.Core部分模块注入


            // 普通服务模块注入
            builder.RegisterModule<BussinessAssociaterEntitiesModule>();



            //autofac使用配置中更改
            var config = new ConfigurationBuilder();// Add the configuration to the ConfigurationBuilder.
            if (File.Exists("autofac.json"))
            {
                config.AddJsonFile("autofac.json");
            }
            var module = new ConfigurationModule(config.Build());// Register the ConfigurationModule with Autofac.
            builder.RegisterModule(module);

            
            //AutoMap的使用（必须再注入完所有引用后使用)
            builder.RegisterModule<AutoMapperManagerModule>();

            BoBContainer.ServiceContainer = builder.Build();
            return builder;
        }

    }
}
