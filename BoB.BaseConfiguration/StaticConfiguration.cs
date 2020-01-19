using BoB.BoBConfiguration.BaseEnums;
using System;
using System.Text;

namespace BoB.BoBConfiguration
{
    public static class StaticConfiguration
    {
        /// <summary>
        /// 当前项目的名称
        /// </summary>
        public const string ProjectName = "BoB";

        /// <summary>
        /// 当前项目的版本
        /// </summary>
        public const string Version = "V1.0";


        public const string BoBConfigFileName = "/BoBConfig.json";

        public const string ModuleBoBConfiguration = "BoBConfiguration";

        public const string ModuleBaseBoBConfiguration = "BaseBoBConfiguration";


        public static LanguageType DefaultLanguage = LanguageType.zh_cn;

        public static string CurrentEnvironment
        {
            get
            {
                if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")))
                {
                    return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                }
                else 
                {
                    //可以添加一个为空的逻辑，告知会使用默认配置（可以使用错误处理模式）
                    return CustomEnvironment.Development;
                }
            }
        }


        public static bool IsDevelopment()
        {
            return CurrentEnvironment == CustomEnvironment.Development;
        }

        public static bool IsProduction()
        {
            return CurrentEnvironment == CustomEnvironment.Production;
        }

        public static bool IsStaging()
        {
            return CurrentEnvironment == CustomEnvironment.Staging;
        }

        public static bool IsLeo()
        {
            return CurrentEnvironment == CustomEnvironment.Leo;
        }

        public static bool IsJim()
        {
            return CurrentEnvironment == CustomEnvironment.Jim;
        }

        public static bool IsWorker()
        {
            return CurrentEnvironment == CustomEnvironment.Worker;
        }

        public static bool IsAdministrator()
        {
            return CurrentEnvironment == CustomEnvironment.Administrator;
        }

    }
}
