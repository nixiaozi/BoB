using Autofac;
using BoB.BaseModule;
using BoB.ExtendAndHelper.Extends;
using System;
using System.Diagnostics;
using System.IO;

namespace BoB.LanguageManager
{
    public class LanguageManagerModule:BoBModule,IBoBModule
    {

        public override void Init(ContainerBuilder builder)
        {
            this.CurrentAssembly = System.Reflection.Assembly.GetExecutingAssembly(); //切换使用当前程序集


            


        }

        public override void OnLoad(ContainerBuilder builder)
        {
            //没有自定义的引用可以不填

        }

    }
}
