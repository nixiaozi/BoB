using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Sunlit.UseBus;

namespace Sunlit.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            BaseRegister.RegisterConfigureContainer(containerBuilder); //添加后台依赖注入
/*  注入服务测试的代码片段
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
