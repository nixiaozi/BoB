﻿using Autofac;
using BoB.BoBLogger;
using BoB.CacheManager;
using BoB.LanguageManager;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;
using BoB.AutoMapperManager;
using BoB.WorldAction;
using BoB.PeopleEntities;
using ACM.UserEntities;
using BoB.EmailManager;

namespace BoB.UseBus.Register
{
    public static class BaseRegister
    {
        public static void RegisterConfigureContainer(ContainerBuilder builder)
        {
            //模块注入,只有注入模块之后，程序集扫描才能获得该assembly
            //BoB.Base部分模块注入
            builder.RegisterModule<BaseModule.BoBModule>();
            builder.RegisterModule<BoBLoggerModule>();
            builder.RegisterModule<CacheManagerModule>();
            builder.RegisterModule<LanguageManagerModule>();
            builder.RegisterModule<EmailManagerModule>();

            //BoB.Core部分模块注入
            //builder.RegisterModule<WorldActionModule>();
            //builder.RegisterModule<PeopleEntitiesModule>();
            builder.RegisterModule<UserEntitiesModule>();
            // AppListEntitiesModule 模块注入 通过autofac.json 配置文件添加


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

        }
    }
}
