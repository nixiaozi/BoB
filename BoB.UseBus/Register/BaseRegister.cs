using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.UseBus.Register
{
    public static class BaseRegister
    {
        public static void RegisterConfigureServices(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();
            //模块注入
            containerBuilder.RegisterModule<BaseModule.BaseModule>();
            containerBuilder.Populate(services);
            containerBuilder.Build();

            //AutoMap的使用

        }
    }
}
