﻿using Autofac;
using BoB.BaseModule;
using System;

namespace ACM.Bilibili
{
    public class BilibiliModule : BoBModule, IBoBModule
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