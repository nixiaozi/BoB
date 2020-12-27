using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACM.AutoWinService
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-5.0&tabs=visual-studio
    /// 上面有这个更加详细的WS实例
    /// </summary>
    public class ACMAutoService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {


            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {


            return Task.CompletedTask;
        }
    }
}
