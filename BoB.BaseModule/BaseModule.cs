using Autofac;
using Autofac.Core;
using Autofac.Features.Metadata;
using BoB.BaseModule.Test.AdaptersandDecorators;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.BaseModule
{
    public class BaseModule:Module,IBaseModule
    {

        public virtual void BeforeLoad()
        {

        }

        public virtual void AfterLoad()
        {

        }

        protected override void Load(ContainerBuilder builder)
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




            builder.RegisterDecorator<LoggingDecorator, ICommandHandler>(); //装饰器是这样会重写RegisterType
            // builder.RegisterDecorator<DiagnosticDecorator, ICommandHandler>();



            builder.RegisterAssemblyTypes(this.ThisAssembly).Where(t => t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces();
            //builder.RegisterType<TestService>().As<ITestService>();



            // builder.Build(); Build() or Update() can only be called once on a ContainerBuilder

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
