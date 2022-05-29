using BoB.ContainManager;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACM.AutoWinService
{
    public static class ServiceBaseLiveTimeHostExtension
    {
        public static IHostBuilder UseServiceBaseLifetime(this
        IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((hostContext,
            services) => services.AddSingleton<IHostLifetime,
            ServiceBaseLifeTime>());
        }
        public static Task RunAsServiceAsync(this IHostBuilder
        hostBuilder, CancellationToken cancellationToken = default)
        {
            
            var builder = hostBuilder.UseServiceBaseLifetime().Build();
            BoBContainer.ServiceContainer = (Autofac.IContainer)builder.Services;  // 需要进行显式类型转化为autofac类型服务容器
            return builder.RunAsync(cancellationToken);
        }
    }

}
