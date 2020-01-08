using BoB.BoBConfiguration;
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

namespace BoB.BoBConfigManager
{
    public class BoBConfigService : IBoBConfigService
    {
        public void DynamicConfigInit()
        {
            var configFilePath = System.Reflection.Assembly.GetEntryAssembly().GetAssemblyRoot() + StaticConfiguration.BoBConfigFileName;
            

            if (File.Exists(configFilePath))
            {
                //如果主路径存在BoBConfig.json文件就直接读取配置文件配置，并覆盖默认配置(配置项不存在则使用默认值)
                Dictionary<string, Dictionary<string,string>> allModuleConfig = null;
                using (StreamReader r = new StreamReader(configFilePath))
                {
                    string json = r.ReadToEnd();
                    allModuleConfig = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
                }


                foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (Type t in a.GetTypes().Where(s => s.Name == StaticConfiguration.ModuleBoBConfiguration))
                    {
                        var theModuleConfig = allModuleConfig.FirstOrDefault(s => s.Key == a.FullName.Substring(0, a.FullName.IndexOf(',')));
                        if (theModuleConfig.Key == null)
                        {
                            continue;
                        }

                        Debug.Write(a.FullName + "\n");
                        Debug.WriteLine(t.Name);

                        //获取所有EditAble标识的字段
                        var typeMembers = t.GetFields().Where(prop => Attribute.IsDefined(prop, typeof(WriteAbleAttribute)));

                        foreach (var item in theModuleConfig.Value)
                        {
                            var theMember = typeMembers.FirstOrDefault(s => s.Name == item.Key);
                            if(theMember!=null)
                                ((FieldInfo)theMember).SetValue(t, item.Value);
                        }

                    }
                }

            }
            else
            {
                Dictionary<string, Dictionary<string, string>> allNewModuleConfig = new Dictionary<string, Dictionary<string, string>>();
                //如果不存在BoBConfig.json文件就直接读取配置生成配置文件
                foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                {
                    var assemblyName = a.FullName.Substring(0, a.FullName.IndexOf(','));
                    Dictionary<string, string> assemblyConfigs = new Dictionary<string, string>();
                    foreach (Type t in a.GetTypes().Where(s => s.Name == StaticConfiguration.ModuleBoBConfiguration))
                    {
                        Debug.Write(a.FullName + "\n");
                        Debug.WriteLine(t.Name);

                        //获取所有EditAble标识的字段
                        var typeMembers = t.GetFields().Where(prop => Attribute.IsDefined(prop, typeof(WriteAbleAttribute)));

                        foreach (var item in typeMembers)
                        {
                            assemblyConfigs.Add(item.Name, item.GetValue(t).ToString());
                        }

                    }
                    if(assemblyConfigs.Count>0)
                        allNewModuleConfig.Add(assemblyName,assemblyConfigs);
                }

                using (var tw = new StreamWriter(configFilePath, true))
                {
                    tw.WriteLine(JsonConvert.SerializeObject(allNewModuleConfig, Formatting.Indented));
                    tw.Close();
                }

            }

            


            

        }
    }
}
