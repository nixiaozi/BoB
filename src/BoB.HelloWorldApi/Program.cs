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
            BaseRegister.RegisterConfigureContainer(containerBuilder); //��Ӻ�̨����ע��

            //�������д����ǣ�
            var serviceCollection = new ServiceCollection();
            IServiceProvider provider= serviceCollection.BuildServiceProvider();

            //var builder = CreateHostBuilder(args).Build();

            //// BoBContainer.ServiceProvider = builder.Services; //���һ����̬������ע���������ã�

            //builder.Run();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // ʹ�õ�����autofacע��⣬����Ҫ��HostBuild���м���
                //.UseServiceProviderFactory(new AutofacServiceProviderFactory()) //���Autofac �����ṩ����
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
#pragma warning restore CS1591
}
