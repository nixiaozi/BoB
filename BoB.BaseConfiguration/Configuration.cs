using System;

namespace BoB.BaseConfiguration
{
    public static class Configuration
    {
        /// <summary>
        /// 当前项目的名称
        /// </summary>
        public const string ProjectName = "ExchangeMall";

        /// <summary>
        /// 当前项目的版本
        /// </summary>
        public const string Version = "V3.0";



        public static string CurrentEnvironment
        {
            get
            {
                return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"); //可以添加一个为空的逻辑，告知会使用默认配置
            }
        }
    }
}
