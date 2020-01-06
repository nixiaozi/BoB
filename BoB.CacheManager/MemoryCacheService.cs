using BoB.BoBConfiguration.BaseEnums;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoB.CacheManager
{
    public class MemoryCacheService : ICacheService
    {
        public MemoryCache Cache { get; set; }

        public MemoryCacheService()
        {
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                // SizeLimit = 1024,
            });
        }

        public T Get<T>(CacheTag tag, string cacheId, Func<T> DataFunc, int? ValidateSecs = null)
        {
            var result = DataFunc.Invoke();
            var fullCacheId = tag + "_" + cacheId;

            if (ValidateSecs.HasValue)
            {
                return Cache.Set<T>(fullCacheId, result, DateTime.Now.AddSeconds(ValidateSecs.Value));
            }
            else
            {
                return Cache.Set<T>(fullCacheId, result);
            }
        }

        public T Get<T>(CacheTag tag, string cacheId, T defaultValue)
        {
            var fullCacheId = tag + "_" + cacheId;

            T result;
            return Cache.TryGetValue<T>(fullCacheId,out result) ? result : defaultValue;
        }

        public void ClearTheCache(CacheTag tag, string cacheId)
        {
            var fullCacheId = tag + "_" + cacheId;
            Cache.Remove(fullCacheId);
        }

        public void ClearTagCache(CacheTag tag)
        {

            throw new NotImplementedException();
            //var alldata = GetAllCache()
            //    .Where(s => s.Key.IndexOf(tag.ToString()) == 0); //获取所有以tag开头的缓存项

            //foreach(var item in alldata)
            //{
            //    Cache.Remove(item.Key);
            //}

        }

        public void ClearAllCache()
        {

            throw new NotImplementedException();
            //var alldata = GetAllCache();
            //foreach (var item in alldata)
            //{
            //    Cache.Remove(item.Key);
            //}
        }

        public Dictionary<string, Object> GetAllCache()
        {
            //不支持可以自定义错误

            throw new NotImplementedException();
        }
    }
}
