using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ACM.AppListEntities;
using BoB.ContainManager;
using ACM.MainDatabase;
using System.Linq;

namespace ACM.AutoWinService
{
    public class AutoTimeService : IHostedService//, IDisposable  
    {
        // private IAppListBlock appListBlock;
        public IServiceProvider CurrentServiceProvider;
        public AutoTimeService(IServiceScopeFactory _serviceScopeFactory)
        {
            CurrentServiceProvider = BoBContainer.ServiceProvider;  // 首先要定义依赖池

            //appListBlock = CurrentServiceProvider.GetService<IAppListBlock>();
            //var apps = appListBlock.GetAllApps();
            // 以上注释代码会导致，在查询数据库时出现context 上下文已释放，查不到数据，
            // 具体可以参考 https://blog.hildenco.com/2018/12/accessing-entity-framework-context-on.html
            // 主要意思是在Controller已关闭或者完成的状态下，EF Core会自动关闭DBContext 连接，释放资源防止资源浪费
            // 但是对于我们这种后台服务，就不能使用这种默认操作
            using (var scope = CurrentServiceProvider.CreateScope())
            {
                var dbContext1 = scope.ServiceProvider.GetService<MaindbContext>();
                var apps1 = dbContext1.Set<AppList>().Where(s => s.ID >= 0).ToList();

                var dbContext2 = scope.ServiceProvider.GetService<MaindbContext>();
                var appListBlock = CurrentServiceProvider.GetService<IAppListBlock>();
                var apps2 = appListBlock.GetAllApps(dbContext2);

                var apps3 = appListBlock.GetAllApps(dbContext1);    // 这个也能获得预期的数据 context 不会自动释放，并且重用也没有关系
                                                                    // context 不进行显式释放也没有关系，因为会在scope之外自动释放
            }


        }

        // 自己继承并这样实现接口 IDisposable 会在退出时打印 stack overflow 溢出
        //public void Dispose()
        //{
        //    this.Dispose();
        //}

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var text = $"{DateTime.Now.ToString("yyyy-MM-dd  HH: mm: ss")}, Testing write." + Environment.NewLine;
            File.WriteAllText(@"D:\Service.Write.txt", text);
            Console.WriteLine($"[{nameof(AutoTimeService)}] has been  started.....");
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            File.Delete(@"D:\Service.Write.txt");
            Console.WriteLine($"[{nameof(AutoTimeService)}] has been stopped.....");

            Thread.Sleep(1000);
            return Task.CompletedTask;
        }

    }
}
