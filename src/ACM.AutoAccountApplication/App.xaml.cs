using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ACM.BaseAutoAction;
using ACM.TaskManager;
using Autofac;
using BoB.UseBus.Register;

namespace ACM.AutoAccountApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IContainer TheContainer;

        public App()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder = BaseRegister.RegisterConfigureContainer(containerBuilder);

            TheContainer = containerBuilder.Build();
        }


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Debug.Print("Todo=====>Start dispatcherTimer_Tick");
            // var _taskManagerService = TheContainer.Resolve<ITaskManagerService>();

            IEnumerable<AutoActionAdapter> _autoActionAdapters = TheContainer.Resolve<IEnumerable<AutoActionAdapter>>();

            _autoActionAdapters.FirstOrDefault(s => s.CommandText == "bilibili")?.DoBrowserRandom(new RandomBrowse());

            Debug.Print("Todo=====>End dispatcherTimer_Tick");

        }

    }
}
