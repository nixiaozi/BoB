using System;
using Autofac;
using BoB.UseBus.Register;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BoB.HelloWorldApi
{
#pragma warning disable CS1591
    public class Program
    {
        public static void Main(string[] args)
        {

            var containerBuilder = new ContainerBuilder();
            BaseRegister.RegisterConfigureContainer(containerBuilder); //添加后台依赖注入

            //下面两行代码是，
            var serviceCollection = new ServiceCollection();
            IServiceProvider provider= serviceCollection.BuildServiceProvider();

            //var builder = CreateHostBuilder(args).Build();

            //// BoBContainer.ServiceProvider = builder.Services; //添加一个静态的依赖注入容器引用，

            //builder.Run();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // 使用单独的autofac注入库，不需要与HostBuild进行集成
                //.UseServiceProviderFactory(new AutofacServiceProviderFactory()) //添加Autofac 服务提供程序
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
#pragma warning restore CS1591
}
