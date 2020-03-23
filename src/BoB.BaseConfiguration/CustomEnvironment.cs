using BoB.ExtendAndHelper.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.BoBConfiguration
{
    public static class CustomEnvironment
    {
        /// <summary>
        /// 开发环境
        /// </summary>
        [Tag("开发环境")]
        public const string Development = "Development";

        /// <summary>
        /// 产品环境
        /// </summary>
        [Tag("产品环境")]
        public const string Production = "Production";

        /// <summary>
        /// 演示环境
        /// </summary>
        [Tag("演示环境")]
        public const string Staging = "Staging";

        // 下面的环境均用于测试
        public const string Leo = "Leo";
        public const string Jim = "Jim";
        public const string Worker = "Worker";
        public const string Administrator = "Administrator";
    }
}
