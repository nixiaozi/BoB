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
            


            base.AttachToComponentRegistration(componentRegistry, registration);
        }


        
        /// <summary>
        /// AttachToRegistrationSource 这个方法会在Configure调用
        /// </summary>
        /// <param name="componentRegistry"></param>
        /// <param name="registrationSource"></param>
        protected override void AttachToRegistrationSource(IComponentRegistry componentRegistry, IRegistrationSource registrationSource)
        {
            base.AttachToRegistrationSource(componentRegistry, registrationSource);
        }

        public new void Configure(IComponentRegistry componentRegistry)
        {
            base.Configure(componentRegistry);
        }

    }
}
