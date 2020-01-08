using Autofac;
using Autofac.Extensions.DependencyInjection;
using BoB.BoBConfigManager;
using BoB.CacheManager;
using BoB.LanguageManager;
using BoB.WorkModule;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.UseBus.Register
{
    public static class BaseRegister
    {
        public static void RegisterConfigureContainer(ContainerBuilder builder)
        {

            //模块注入,只有注入模块之后，程序集扫描才能获得该assembly
            builder.RegisterModule<BaseModule.BoBModule>();
            // builder.RegisterModule<BoBConfigManagerModule>();
            builder.RegisterModule<CacheManagerModule>();
            builder.RegisterModule<LanguageManagerModule>();
            builder.RegisterModule<WorkModule.WorkModule>();

            // var container = builder.Build(); 使用新的注入方法，不需要其他了

            //AutoMap的使用

        }
    }
}
