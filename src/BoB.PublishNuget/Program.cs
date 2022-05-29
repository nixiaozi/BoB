using System;
using System.Diagnostics;
using System.IO;

namespace BoB.PublishNuget
{
    internal class Program
    {
        private static string NugetKey = "oy2p2t7isasv2tvkytza6k4zlbfbdvru3iqqwtkb676mdq";
        static void Main(string[] args)
        {

            // 本地文件搜索
            string rootdir = Path.GetFullPath(@"D:\MyProject\BoB\src\ ");
            string[] files = Directory.GetFiles(rootdir, "*.nupkg", SearchOption.AllDirectories);


            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true; // 输入也要监视
            info.UseShellExecute = false;

            p.StartInfo = info;
            p.Start();

            // 命令 echo
            using (StreamWriter sw = p.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    foreach(string fileName in files)
                    {
                        var cmdText = string.Format(@"dotnet nuget push {0} --api-key {1} --source  https://api.nuget.org/v3/index.json ", fileName, NugetKey);
                        sw.WriteLine(cmdText);
                        sw.WriteLine(Environment.NewLine); // 提交命令
                        System.Threading.Thread.Sleep(100);
                    }


                    // 查看命令输出
                    //Console.WriteLine(p.StandardOutput.ReadLine());
                    //Console.WriteLine(p.StandardOutput.ReadLine());
                    System.Threading.Thread.Sleep(10000);
                    var outputStr = p.StandardOutput.ReadLine();
                    var falge = true;
                    while (outputStr != null && falge)
                    {
                        Console.WriteLine(outputStr);
                        if (outputStr == "ToDo The CMD Output1")
                        {
                            // 表示已经执行完毕
                            falge = false;
                            Console.WriteLine("命令 echo ToDo The CMD Output1  已执行完毕");
                        }


                        outputStr = p.StandardOutput.ReadLine();
                    }


                }
            }

            Console.WriteLine("Done");


        }
    }
}
