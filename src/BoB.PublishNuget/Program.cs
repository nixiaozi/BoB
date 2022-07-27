using System;
using System.Diagnostics;
using System.IO;

namespace BoB.PublishNuget
{
    internal class Program
    {
        // oy2hzzkl2dhtfdwwelzqsajdbokvwlxevdplqo44lbp5ri
        private static string NugetKey = "oy2hzzkl2dhtfdwwelzqsajdbokvwlxevdplqo44lbp5ri";
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

                    // 添加自定义 Echo 然后
                    sw.WriteLine("echo ToDo The CMD Output1");
                    sw.WriteLine(Environment.NewLine);

                    // 查看命令输出
                    //Console.WriteLine(p.StandardOutput.ReadLine());
                    //Console.WriteLine(p.StandardOutput.ReadLine());
                    System.Threading.Thread.Sleep(300);
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


                    sw.WriteLine(@"del /s D:\MyProject\BoB\src\*.nupkg");
                    sw.WriteLine(Environment.NewLine);

                }
            }

            Console.WriteLine("Done");


        }
    }
}
