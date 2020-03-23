using Autofac;
using BoB.BaseModule;
using ExtendAndHelper.Extends;
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


            //一下两个路径都是最后dll存在位置的路径，不是代码源文件的路径
            //var rootPath = System.Reflection.Assembly.GetEntryAssembly().GetAssemblyRoot()+ "/Lang";
            //var currentPath = System.Reflection.Assembly.GetExecutingAssembly().GetAssemblyRoot()+ "/Lang";
            //Debug.WriteLine("RootPath:"+rootPath.ToString());
            //Debug.WriteLine("CurrentPath:" + currentPath);
            ////首先需要知道主目录是否存在Lang文件夹
            //if (!Directory.Exists(rootPath))
            //{
            //    //不存在的话需要从当前复制
            //    if(Directory.Exists(currentPath))
            //    {
            //        string[] files = Directory.GetFiles(currentPath);
            //        foreach (string s in files)
            //        {
            //            // Use static Path methods to extract only the file name from the path.
            //            var fileName = Path.GetFileName(s);
            //            var destFile = Path.Combine(rootPath, fileName);
            //            System.IO.File.Copy(s, destFile, true);
            //        }

            //    }
            //    else
            //    {
            //        //可以添加自定义报错

            //    }

            //}

        }

    }
}
