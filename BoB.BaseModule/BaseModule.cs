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


            builder.RegisterDecorator(typeof(LoggingDecorator), typeof(ICommandHandler));
            builder.RegisterDecorator(typeof(DiagnosticDecorator), typeof(ICommandHandler));


            builder.RegisterAdapter<Meta<ICommand>, ToolbarButton>(
                cmd => new ToolbarButton(cmd.Value, (string)cmd.Metadata["Name"]));

            builder.RegisterType<TestService>().As<ITestService>();

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
