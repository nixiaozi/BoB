using Autofac;
using Autofac.Extensions.DependencyInjection;
using BoB.BoBConfigManager;
using BoB.BoBLogger;
using BoB.CacheManager;
using BoB.LanguageManager;
using BoB.WorkModule;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;
using BoB.AutoMapperManager;

namespace BoB.UseBus.Register
{
    public static class BaseRegister
    {
        public static void RegisterConfigureContainer(ContainerBuilder builder)
        {
            //模块注入,只有注入模块之后，程序集扫描才能获得该assembly
            builder.RegisterModule<BaseModule.BoBModule>();
            builder.RegisterModule<BoBLoggerModule>();
            // builder.RegisterModule<BoBConfigManagerModule>(); 已经使用静态构造函数初始化，不需要这种注入操作了
            builder.RegisterModule<CacheManagerModule>();
            builder.RegisterModule<LanguageManagerModule>();
            builder.RegisterModule<WorkModule.WorkModule>();

            // var container = builder.Build(); 使用新的注入方法，不需要其他了



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
