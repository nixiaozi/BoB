using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ACM.BaseAutoAction;
using ACM.TaskManager;
using Autofac;
using BoB.ContainManager;
using BoB.UseBus.Register;

namespace ACM.AutoAccountApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IContainer TheContainer;
        DispatcherTimer dispatcherTimer;
        

        public App()
        {
            dispatcherTimer = new DispatcherTimer();
            var containerBuilder = new ContainerBuilder();
            BaseRegister.RegisterConfigureContainer(containerBuilder); //添加后台依赖注入
            TheContainer = BoBContainer.ServiceContainer;
            //var containerBuilder = new ContainerBuilder();
            //containerBuilder = BaseRegister.RegisterConfigureContainer(containerBuilder);

            //TheContainer = containerBuilder.Build();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            // Perform tasks at application exit
            // 添加计时器并等待任务都成功取消
            // 对每个运行中的Task运行cancel方法
            // 并且等待直到所有的任务已取消或已完成并修改数据库状态
            // 最后停掉计时器， 



            dispatcherTimer.Stop();


        }


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            RuntimeContext.Instance.InitTaskBefore();

            // DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();

            // for test
            

            // 自动迭代正在执行的任务列表处理任务状态
            //RuntimeContext.Instance.HandlerDoingTaskAction();


            //// 添加最高等级任务
            //// 根据情况添加其他等级任务
            //RuntimeContext.Instance.DoingPrepareTask();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Debug.Print("Todo=====>Start dispatcherTimer_Tick");
            //// var _taskManagerService = TheContainer.Resolve<ITaskManagerService>();

            //IEnumerable<AutoActionAdapter> _autoActionAdapters = TheContainer.Resolve<IEnumerable<AutoActionAdapter>>();

            //_autoActionAdapters.FirstOrDefault(s => s.CommandText == "bilibili")?.DoBrowserRandom(new RandomBrowse());

            // 自动迭代正在执行的任务列表处理任务状态
            RuntimeContext.Instance.HandlerDoingTaskAction();


            // 添加最高等级任务
            // 根据情况添加其他等级任务
            RuntimeContext.Instance.DoingPrepareTask(null,true); // 这里只需要合并默认的



            Debug.Print("Todo=====>End dispatcherTimer_Tick");

        }


        // 自动迭代正在执行的任务列表处理任务状态



    }
}
