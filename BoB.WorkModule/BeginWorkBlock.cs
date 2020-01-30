using Autofac;
using BoB.BaseModule;
using BoB.ContainManager;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using BoB.BaseModule.Test.AdaptersandDecorators;
using System.Linq;
using BoB.BaseModule.Test.TypeInject;
using BoB.MainDataBase;
using ExtendAndHelper.Utilties;
using BoB.AutoMapperManager;

namespace BoB.Work
{
    public class BeginWorkBlock: BaseBlock, IBeginWorkBlock // 需要 BeginWorkBlock 和 IBeginWorkBlock 都以Service结尾，否者程序集扫描不会引用注入
    {
        private ITestService _testService;
        private IEnumerable<ToolbarButton> _toolbarButtons;
        private IAutoMapperService _autoMapperService;

        protected override void Init()
        {
            _autoMapperService= CurrentServiceProvider.GetService<IAutoMapperService>();
            // var ServiceProvider = BoBContainer.ServiceProvider; 通过基类获取
            //for(var i = 1; i < 100000000; i++)
            //{
                _testService = CurrentServiceProvider.GetService<ITestService>(); // 必须使用Microsoft.Extensions.DependencyInjection 否则不能解释GetService<T> 泛型方法
                _testService.SayHello();
            //}


            //for (var i = 1; i < 100000000; i++)
            //{
            //using (var scope = CurrentServiceProvider.CreateScope())  //这里使用了生命周期
            //{
            //    scope.ServiceProvider.GetService<ITestService>().Say("Current Invoke:" + " time");
            //}
            //}
            var context = new MainDbContext();
            var id = context.Set<School>().Count()+100;  //可以使用该方法获模型数据
            var test = context.Add<School>(new School { SchoolID = id, Location = RandomHelper.RandomString(20), SchoolName = RandomHelper.RandomString(10) });
            context.SaveChanges();
            

            _toolbarButtons = CurrentServiceProvider.GetService<IEnumerable<ToolbarButton>>();

            CurrentServiceProvider.GetService<TypeInjectTest>().ToInjectTest(); //直接从注入库中拿出对象
        }


        public void CheckSex()
        {
            _testService.Say("CheckSex");
            _toolbarButtons.FirstOrDefault(s => s.CommandText == "Open File")?.Click();


            var school = GetSchool(110);
            var schooldto = _autoMapperService.DoMap<School, SchoolDto>(school);

             School mSchllo = _autoMapperService.DoMap<SchoolDto, School>(schooldto);

        }


        public School GetSchool(int ID)
        {
            var context = new MainDbContext();
            return context.Set<School>().FirstOrDefault(s => s.SchoolID == ID);
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
