using BoB.ContainManager;
using ExtendAndHelper.CustomAttributes;
using ExtendAndHelper.Extends;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BoB.BoBConfiguration
{
    public static class BaseBoBConfiguration
    {
        public static readonly Assembly CurrentAssembly = Assembly.GetExecutingAssembly();

        //静态构造函数可以，初始化readonly
        static BaseBoBConfiguration()
        {
            Init(CurrentAssembly);
        }

        private static object obj = new object();

        public static void Init(Assembly theAssembly)
        {
            var configFilePath = System.Reflection.Assembly.GetEntryAssembly().GetAssemblyRoot() + StaticConfiguration.BoBConfigFileName;

            lock (obj)
            {

                if (File.Exists(configFilePath))
                {

                    //如果主路径存在BoBConfig.json文件就直接读取配置文件配置，并覆盖默认配置(配置项不存在则使用默认值)
                    Dictionary<string, Dictionary<string, string>> allModuleConfig = null;
                    using (StreamReader r = new StreamReader(configFilePath))
                    {
                        string json = r.ReadToEnd();
                        allModuleConfig = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
                    }



                    foreach (Type t in theAssembly.GetTypes().Where(s => s.Name == StaticConfiguration.ModuleBoBConfiguration || s.Name == StaticConfiguration.ModuleBaseBoBConfiguration))
                    {
                        var theModuleConfig = allModuleConfig.FirstOrDefault(s => s.Key == theAssembly.FullName.Substring(0, theAssembly.FullName.IndexOf(',')));
                        if (theModuleConfig.Key == null)
                        {
                            //需要修改文件添加配置节
                            foreach(var item in GetNewModuleConfig(theAssembly))
                            {
                                allModuleConfig.Add(item.Key,item.Value);
                            }

                            File.Delete(configFilePath);

                            using (var tw = new StreamWriter(configFilePath, true))
                            {
                                tw.WriteLine(JsonConvert.SerializeObject(allModuleConfig, Formatting.Indented));
                                tw.Close();
                            }

                            continue;
                        }

                        //Debug.Write(theAssembly.FullName + "\n");
                        //Debug.WriteLine(t.Name);

                        //获取所有EditAble标识的字段
                        var typeMembers = t.GetFields().Where(prop => Attribute.IsDefined(prop, typeof(WriteAbleAttribute)));

                        foreach (var item in theModuleConfig.Value)
                        {
                            var theMember = typeMembers.FirstOrDefault(s => s.Name == item.Key);
                            if (theMember != null)
                                ((FieldInfo)theMember).SetValue(t, item.Value);
                        }

                    }


                }
                else
                {
                    //如果不存在BoBConfig.json文件就直接读取配置生成配置文件

                    Dictionary<string, Dictionary<string, string>> allNewModuleConfig = GetNewModuleConfig(theAssembly);


                    using (var tw = new StreamWriter(configFilePath, true))
                    {
                        tw.WriteLine(JsonConvert.SerializeObject(allNewModuleConfig, Formatting.Indented));
                        tw.Close();
                    }

                }
            }
        }

        private static Dictionary<string, Dictionary<string, string>> GetNewModuleConfig(Assembly theAssembly)
        {
            Dictionary<string, Dictionary<string, string>> allNewModuleConfig = new Dictionary<string, Dictionary<string, string>>();


            var assemblyName = theAssembly.FullName.Substring(0, theAssembly.FullName.IndexOf(','));
            Dictionary<string, string> assemblyConfigs = new Dictionary<string, string>();
            foreach (Type t in theAssembly.GetTypes().Where(s => s.Name == StaticConfiguration.ModuleBoBConfiguration || s.Name == StaticConfiguration.ModuleBaseBoBConfiguration))
            {
                //Debug.Write(theAssembly.FullName + "\n");
                //Debug.WriteLine(t.Name);

                //获取所有EditAble标识的字段
                var typeMembers = t.GetFields().Where(prop => Attribute.IsDefined(prop, typeof(WriteAbleAttribute)));

                foreach (var item in typeMembers)
                {
                    assemblyConfigs.Add(item.Name, item.GetValue(t).ToString());
                }

            }
            if (assemblyConfigs.Count > 0)
                allNewModuleConfig.Add(assemblyName, assemblyConfigs);

            return allNewModuleConfig;
        }


        [WriteAble]
        public static readonly string Test = "Fast";

    }
}
