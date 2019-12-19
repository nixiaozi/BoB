using Autofac;
using Autofac.Core;

namespace BoB.BaseModule
{
    public class BaseModule:Module,IBaseModule
    {
        public virtual System.Reflection.Assembly CurrentAssembly { get; set; } = null;

        protected override void Load(ContainerBuilder builder)
        {
            CurrentAssembly = CurrentAssembly == null ? ThisAssembly : CurrentAssembly;
            builder.RegisterAssemblyTypes(CurrentAssembly)
                .Where(t => t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces();
            base.Load(builder);
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
