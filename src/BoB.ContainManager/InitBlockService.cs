﻿using System;

namespace BoB.ContainManager
{
    public class InitBlockService
    {
        protected readonly IServiceProvider CurrentServiceProvider;

        public InitBlockService()
        {
            CurrentServiceProvider = BoBContainer.ServiceProvider;

            Init();

            //这里可以在服务解析之后进行的基类处理
        }


        protected virtual void Init()
        {
            
        }


    }
}