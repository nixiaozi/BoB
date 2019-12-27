using Autofac;
using Autofac.Extensions.DependencyInjection;
using BoB.LanguageManager;
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

            //模块注入
            builder.RegisterModule<BaseModule.BoBModule>();
            builder.RegisterModule<LanguageManagerModule>();


            // var container = builder.Build(); 使用新的注入方法，不需要其他了

            //AutoMap的使用
        }
    }
}
