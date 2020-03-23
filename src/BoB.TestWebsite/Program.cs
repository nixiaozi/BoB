using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BoB.BaseModule;
using BoB.BoBConfigManager;
using BoB.ContainManager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BoB.TestWebsite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();

            BoBContainer.ServiceProvider = builder.Services; //���һ����̬������ע���������ã�


            //var test = BoBContainer.ServiceProvider.GetService<ITestService>();
            //test.SayHello();
            //�������ɨ��������û�ȡ
            //BoBContainer.ServiceProvider.GetService<IBoBConfigService>().DynamicConfigInit();




            builder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory()) //���Autofac �����ṩ����
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
