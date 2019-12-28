using Autofac;
using BoB.BaseModule;
using BoB.ContainManager;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace BoB.Work
{
    public class BeginWorkService: BaseFlow, IBeginWorkService // 需要BeginWorkService和IBeginWorkService都以Service结尾，否者程序集扫描不会引用注入
    {
        private ITestService _testService;

        protected override void Init()
        {
            // var ServiceProvider = BoBContainer.ServiceProvider; 通过基类获取

            _testService = CurrentServiceProvider.GetService<ITestService>(); // 必须使用Microsoft.Extensions.DependencyInjection 否则不能解释GetService<T> 泛型方法

            _testService.SayHello();
        }


        public void CheckSex()
        {
            _testService.Say("CheckSex");
        }

        public void CheckWord()
        {
            _testService.Say("CheckWord");
        }

        public void Save()
        {
            _testService.Say("Save");
        }

        public void Send()
        {
            _testService.Say("Send");
        }


    }
}
