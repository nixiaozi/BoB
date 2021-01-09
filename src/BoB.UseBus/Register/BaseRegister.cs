using Autofac;
using BoB.BoBLogger;
using BoB.CacheManager;
using BoB.LanguageManager;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;
using BoB.AutoMapperManager;
using BoB.WorldAction;
using BoB.PeopleEntities;
using ACM.UserEntities;
using BoB.EmailManager;
using Autofac.Features.Metadata;
using ACM.BaseAutoAction;
using ACM.SinaChina;
using ACM.Bilibili;
using ACM.TaskManager;

namespace BoB.UseBus.Register
{
    public static class BaseRegister
    {
        // 为了兼容直接使用autofac 容器的情况增加了函数返回值
        public static ContainerBuilder RegisterConfigureContainer(ContainerBuilder builder)
        {
            //模块注入,只有注入模块之后，程序集扫描才能获得该assembly
            //BoB.Base部分模块注入
            builder.RegisterModule<BaseModule.BoBModule>();
            builder.RegisterModule<BoBLoggerModule>();
            builder.RegisterModule<CacheManagerModule>();
            builder.RegisterModule<LanguageManagerModule>();
            builder.RegisterModule<EmailManagerModule>();

            //BoB.Core部分模块注入
            //builder.RegisterModule<WorldActionModule>();
            //builder.RegisterModule<PeopleEntitiesModule>();
            builder.RegisterModule<UserEntitiesModule>();
            // AppListEntitiesModule 模块注入 通过autofac.json 配置文件添加
            builder.RegisterModule<TaskManagerModule>();


            //autofac使用配置中更改
            var config = new ConfigurationBuilder();// Add the configuration to the ConfigurationBuilder.
            if (File.Exists("autofac.json"))
            {
                config.AddJsonFile("autofac.json");
            }
            var module = new ConfigurationModule(config.Build());// Register the ConfigurationModule with Autofac.
            builder.RegisterModule(module);

            //需要添加对于不同APP浏览效果的适配器【注意： 这个有可能会在BaseModule之前执行】
            //注意不要以特殊后缀Service 和 Block 结尾，因为这样autofac会自动再进行一次自动注入，破坏我们的适配器设置
            builder.RegisterType<SinaChinaAuto>()
                .As<IBaseAuto>()
                .WithMetadata("type", "sinachina");

            builder.RegisterType<BilibiliAuto>()
                .As<IBaseAuto>()
                .WithMetadata("type", "bilibili");


            builder.RegisterAdapter<Meta<IBaseAuto>, AutoActionAdapter>(
                cmd => new AutoActionAdapter(cmd.Value, (string)cmd.Metadata["type"]));

            //AutoMap的使用（必须再注入完所有引用后使用)
            builder.RegisterModule<AutoMapperManagerModule>();


            return builder;
        }
    }
}
