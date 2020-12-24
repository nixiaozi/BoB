using Autofac;
using BoB.BaseModule;

namespace BoB.EmailManager
{
    public class EmailManagerModule : BoBModule, IBoBModule
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
