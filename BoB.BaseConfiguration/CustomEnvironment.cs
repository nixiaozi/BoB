using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.BaseConfiguration
{
    public static class CustomEnvironment
    {
        /// <summary>
        /// 开发环境
        /// </summary>
        public const string Development = "Development";

        /// <summary>
        /// 产品环境
        /// </summary>
        public const string Production = "Production";

        /// <summary>
        /// 演示环境
        /// </summary>
        public const string Staging = "Staging";

        // 下面的环境均用于测试
        public const string Leo = "Leo";
        public const string Jim = "Jim";
        public const string Worker = "Worker";
        public const string Administrator = "Administrator";
    }
}
