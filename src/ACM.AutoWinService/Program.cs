using ACM.AppListEntities;
using ACM.MainDatabase;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BoB.BoBContainManager;
using BoB.UseBus.Register;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ACM.AutoWinService
{
    class Program
    {
        /// <summary>
        /// 创建 Windows Service 的参考链接是 https://tocalai.medium.com/create-windows-service-using-net-core-console-application-dc2f278bbe42
        /// 以下是创建服务成功的命令 服务名：ACMService 路径：d:\publish\ACM Service\ACM.AutoWinService.exe
        /// sc.exe create ACMService binPath= "d:\publish\ACM Service\ACM.AutoWinService.exe"
        /// Important：在使用Windows Service 时 autofac.json 添加的配置似乎不可用，应该是由于账号没有权限访问造成的
        /// 可以直接在Programming中添加
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            // Run with console or service
            var asService = !(Debugger.IsAttached || args.Contains("-- console"));

            var builder = new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                // 同时只能存在一个HostedService，默认会解析第一次遇到的这个，而忽略后面的
                //services.AddHostedService<AutoTimeService>();
                services.AddHostedService<ACMAutoService>();
            })
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())  // 添加Autofac 服务提供程序;
            .ConfigureContainer<ContainerBuilder>(af =>
            {
                BaseRegister.RegisterConfigureContainer(af);
                //af.RegisterInstance<MaindbContext>(new MaindbContext());
                af.RegisterType<MaindbContext>(); // 添加对数据库访问类的实例引用
                af.RegisterModule<AppListEntitiesModule>();// 添加对业务模块的引用
            }); // 然后使用autofac容器进行配置


            builder.UseEnvironment(asService ? Environments.Production : Environments.Development);

            //var buildered = builder;
            //BoBContainer.ServiceProvider = buildered.Build().Services;

            if (asService)
            {
                await builder.RunAsServiceAsync();
            }
            else
            {
                // await builder.RunConsoleAsync();
                var buildered = builder.Build();
                await buildered.RunAsync();
            }
        }
    }
}
