using BoB.BoBConfiguration.BaseEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.LanguageManager
{
    public interface ILangService
    {
        /// <summary>
        /// 翻译并获取译文
        /// </summary>
        /// <param name="OriginalText"></param>
        /// <param name="langType"></param>
        /// <returns></returns>
        public string L(string OriginalText, LanguageType? langType=null);
    }
}
