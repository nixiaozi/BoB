using Autofac;
using Autofac.Core;
using Autofac.Features.Metadata;
using BoB.BoBConfiguration;
using BoB.BaseModule.Test.AdaptersandDecorators;
using BoB.BaseModule.Test.TypeInject;
using ExtendAndHelper;
using ExtendAndHelper.Extends;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BoB.BaseModule
{
    public class BoBModule:Module,IBoBModule
    {

        public System.Reflection.Assembly CurrentAssembly = null;

        public virtual void Init(ContainerBuilder builder)
        {
           var EnvName= Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"); //使用autofac获取当前环境的方法


            Debug.WriteLine("Load".ToString().AddBeforeStr("To"));
            Debug.WriteLine("当前的部署环境为Leo?：" + "Current EnvironmentName:" + EnvName);
            Debug.WriteLine("当前的部署环境为"+ Configuration.CurrentEnvironment);
            Debug.WriteLine("当前的部署环境为" + Environment.GetEnvironmentVariable("environment_name")); //不存在此环境变量，所以为空
        }

        public virtual void OnLoad(ContainerBuilder builder)
        {
            //builder.RegisterAdapter
            base.Load(builder);

            // Register the services to be adapted
            builder.RegisterType<SaveCommand>()
                   .As<ICommand>()
                   .WithMetadata("Name", "Save File");
            builder.RegisterType<OpenCommand>()
                   .As<ICommand>()
                   .WithMetadata("Name", "Open File");

            builder.RegisterAdapter<Meta<ICommand>, ToolbarButton>(
                cmd => new ToolbarButton(cmd.Value, (string)cmd.Metadata["Name"]));



            builder.RegisterType<SaveCommandHandler>()
                    .As<ICommandHandler>();
            builder.RegisterType<OpenCommandHandler>()
                   .As<ICommandHandler>();


            builder.RegisterType<TypeInjectTest>(); //注入对象
            builder.RegisterType<TypeInjectTest>().As<IinjectTest>(); //接口注入
            builder.RegisterType<Todo>().As<ITodo>(); //注入接口


            builder.RegisterDecorator<LoggingDecorator, ICommandHandler>(); //装饰器是这样会重写RegisterType（所有实现ICommandHandler接口的方法）
            // builder.RegisterDecorator<DiagnosticDecorator, ICommandHandler>(); 


            //builder.RegisterType<TestService>().As<ITestService>();



            // builder.Build(); Build() or Update() can only be called once on a ContainerBuilder

        }

        protected override void Load(ContainerBuilder builder)
        {
            Init(builder);
            builder.RegisterAssemblyTypes(CurrentAssembly!=null? CurrentAssembly : this.ThisAssembly)
                .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Block"))
                    .AsImplementedInterfaces();
            OnLoad(builder);
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            //这里是读取一个自定义的实现某接口的文件，进行批量化注入
            //调用Load方法之后调用


            base.AttachToComponentRegistration(componentRegistry, registration);
        }


        
        /// <summary>
        /// AttachToRegistrationSource 这个方法会在Configure调用
        /// </summary>
        /// <param name="componentRegistry"></param>
        /// <param name="registrationSource"></param>
        protected override void AttachToRegistrationSource(IComponentRegistry componentRegistry, IRegistrationSource registrationSource)
        {
            //方法参数同AttachToComponentRegistration
            //在AttachToComponentRegistration后调用，会调用多次
            //在Configure方法初始化完成后也会再调用一次

            base.AttachToRegistrationSource(componentRegistry, registrationSource);
        }

        public new void Configure(IComponentRegistry componentRegistry)
        {
            base.Configure(componentRegistry);
        }

    }
}
