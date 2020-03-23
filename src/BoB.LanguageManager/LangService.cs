using BoB.BoBConfiguration.BaseEnums;
using BoB.CacheManager;
using BoB.ExtendAndHelper.CustomAttributes;
using BoB.ExtendAndHelper.Utilties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
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
        public string L(string OriginalText,LanguageType? langType=null)
        {
            var theLangType = langType.HasValue ? langType.Value : BoBConfiguration.BaseLangType; 
            var LangDics = _cacheService.Get<Dictionary<string, string>>(CacheTag.BoBLangService, theLangType.ToString(),
                () =>
                {
                    var rootPath = PathHelper.GetAppRootPath();
                    var filePath = rootPath + BoBConfiguration.LangFolder + theLangType.ToString() + ".json";
                    if (Directory.Exists(rootPath+ BoBConfiguration.LangFolder) && File.Exists(filePath))
                    {
                        using (StreamReader r = new StreamReader(filePath,Encoding.UTF8)) 
                        {
                            string json = r.ReadToEnd();
                            Dictionary<string, string> items = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                            return items;
                        }

                    }
                    else
                    {
                        return new Dictionary<string, string>();
                    }

                },3600);

            return LangDics.ContainsKey(OriginalText) ? LangDics[OriginalText] : OriginalText; //如果找不到译文则直接显示原文
        }

    }
}
