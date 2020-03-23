using Autofac;
using Autofac.Core;

namespace BoB.BaseModule
{
    public class BoBModule:Module,IBoBModule
    {

        public System.Reflection.Assembly CurrentAssembly = null;

        public virtual void Init(ContainerBuilder builder)
        {

        }

        public virtual void OnLoad(ContainerBuilder builder)
        {
            //builder.RegisterAdapter
            base.Load(builder);

        

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
