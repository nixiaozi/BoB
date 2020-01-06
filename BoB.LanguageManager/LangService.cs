using BoB.BoBConfiguration.BaseEnums;
using BoB.CacheManager;
using ExtendAndHelper.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

namespace BoB.LanguageManager
{
    public class LangService:ILangService
    {
        private ICacheService _cacheService;
        public LangService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        /// <summary>
        /// 翻译文本
        /// </summary>
        /// <param name="OriginalText">原文</param>
        /// <param name="langType">翻译语种</param>
        /// <returns></returns>
        public string L(string OriginalText,LanguageType? langType)
        {
            var theLangType = langType.HasValue ? langType.Value : LanguageType.zh_cn;
            var LangDics = _cacheService.Get<Dictionary<string, string>>(CacheTag.BoBConfig, theLangType.ToString(),
                () =>
                {

                    return new Dictionary<string, string>();
                },3600);

            return "";
        }

    }
}
