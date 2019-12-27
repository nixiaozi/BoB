using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.LanguageManager
{
    public interface ILangService
    {
        /// <summary>
        /// 获取可显示的文本标签
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public string TD(string tag);

        /// <summary>
        /// 获取翻译后的文本标签
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="languageType"></param>
        /// <returns></returns>
        public string TL(string tag, LanguageType? languageType = null);
    }
}
