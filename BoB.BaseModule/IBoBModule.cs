using Autofac;
using Autofac.Core;
using System;
using System.Reflection;

namespace BoB.BaseModule
{
    public interface IBoBModule : IModule
    {
        /// <summary>
        /// 注入前的初始化
        /// </summary>
        /// <param name="builder"></param>
        public void Init(ContainerBuilder builder);

        /// <summary>
        /// 模块的注入方法
        /// </summary>
        /// <param name="builder"></param>
        public void OnLoad(ContainerBuilder builder);
    }
}
