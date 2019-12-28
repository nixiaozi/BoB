using Autofac;
using BoB.BaseModule;
using BoB.ContainManager;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace BoB.Work
{
    public class BeginWork
    {
        private ITestService _testService;

        public BeginWork()
        {
            var ServiceProvider = BoBContainer.ServiceProvider;

            _testService = ServiceProvider.GetService<ITestService>(); // 必须使用Microsoft.Extensions.DependencyInjection 否则不能解释GetService<T> 泛型方法

            _testService.SayHello();
        }

    }
}
