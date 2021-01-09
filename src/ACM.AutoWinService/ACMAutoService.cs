using ACM.BaseAutoAction;
using ACM.MainDatabase;
using ACM.TaskManager;
using ACM.TaskManager.Model;
using BoB.ContainManager;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACM.AutoWinService
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-5.0&tabs=visual-studio
    /// 上面有这个更加详细的WS实例
    /// </summary>
    public class ACMAutoService : IHostedService, IDisposable
    {
        private List<int> TaskList;

        private int executionCount = 0;
        private readonly ILogger<ACMAutoService> _logger;
        private Timer _timer;
        public IServiceProvider CurrentServiceProvider;
        private IEnumerable<AutoActionAdapter> _autoActionAdapters;

        public ACMAutoService(ILogger<ACMAutoService> logger)
        {
            _logger = logger;
            CurrentServiceProvider= BoBContainer.ServiceProvider;
            _autoActionAdapters = CurrentServiceProvider.GetService<IEnumerable<AutoActionAdapter>>();
        }



        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            // 下面定义了一个周期执行的任务
            //_timer = new Timer(DoWork, null, TimeSpan.Zero,
            //    TimeSpan.FromSeconds(5)); // 
            // test
            _autoActionAdapters.FirstOrDefault(s => s.CommandText == "bilibili")?.DoBrowserRandom(new RandomBrowse());


            _timer =new Timer(DoWork,cancellationToken,TimeSpan.Zero,
                TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }



        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);

            CancellationToken serviceCancelToken = (CancellationToken)state;

            // 首先获取所有正在执行的任务列表
            // 对于已完成的任务 添加数据库完成标记，并在列表中删除对象


            // 如果队列没有富余，就需要对执行的时间进行查看，超过半小时就会自动终止
            // 如果队列有富余那么，就首先查看是否有可以执行的高级别任务


            //
            
                

                //var IsDo = true;
                //while (IsDo&&(this.TaskList==null ||this.TaskList.Count<4))
                //{
                //    _autoActionAdapters.FirstOrDefault(s => s.CommandText == "bilibili")?.DoBrowserRandom(new RandomBrowse());

                //    TaskList.Add(TaskList == null?1:TaskList.Count+1);
                //}



        }

    }
}
