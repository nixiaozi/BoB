using Autofac;
using AutoMapper;
using BoB.BaseModule;
using BoB.BoBConfiguration;
using System;
using System.Linq;
using System.Reflection;

namespace BoB.AutoMapperManager
{
    public class AutoMapperManagerModule : BoBModule, IBoBModule
    {
        public override void Init(ContainerBuilder builder)
        {
            this.CurrentAssembly = System.Reflection.Assembly.GetExecutingAssembly(); //切换使用当前程序集
        }

        public override void OnLoad(ContainerBuilder builder)
        {
            

        }
    }
}
