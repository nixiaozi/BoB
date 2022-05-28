using Autofac;
using BoB.ContainManager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sunlit.BussinessAssociaterEntities;
using Sunlit.UseBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunlit.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            BaseRegister.RegisterConfigureContainer(containerBuilder); //��Ӻ�̨����ע��
/*  ע�������ԵĴ���Ƭ��
            var theserver = BoBContainer.ServiceContainer.Resolve<IBussinessAssociaterBlock>();
            theserver.AsyncGetList(s=>1==1);
*/
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
