using Autofac;
using AutofacSerilogIntegration;
using BoB.BaseModule;
using Serilog;
using System;

namespace BoB.BoBLogger
{
    public class BoBLoggerModule:BoBModule,IBoBModule
    {
        public override void Init(ContainerBuilder builder)
        {
            this.CurrentAssembly = System.Reflection.Assembly.GetExecutingAssembly(); //切换使用当前程序集
        }

        public override void OnLoad(ContainerBuilder builder)
        {
            //没有自定义的引用可以不填
            //DevelopSerilog.Configuration().Error("Directly Serilog Error!");
            //builder.RegisterLogger(DevelopSerilog.Configuration()); //add autofac for for Serilog
            //builder.Register<ILogger>((c, p) =>
            //{
            //    return new LoggerConfiguration()
            //      .WriteTo.RollingFile(
            //        AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "/Log-{Date}.txt")
            //      .CreateLogger();
            //}).SingleInstance();
        }
    }
}
