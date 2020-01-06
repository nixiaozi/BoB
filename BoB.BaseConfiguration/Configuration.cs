using BoB.BoBConfiguration.BaseEnums;
using System;

namespace BoB.BoBConfiguration
{
    public static class Configuration
    {
        /// <summary>
        /// 当前项目的名称
        /// </summary>
        public const string ProjectName = "BasicofBasic";

        /// <summary>
        /// 当前项目的版本
        /// </summary>
        public const string Version = "V1.0";


        public static LanguageType DefaultLanguage = LanguageType.zh_cn;

        public static string CurrentEnvironment
        {
            get
            {
                return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"); //可以添加一个为空的逻辑，告知会使用默认配置（可以使用错误处理模式）
            }
        }
    }
}
