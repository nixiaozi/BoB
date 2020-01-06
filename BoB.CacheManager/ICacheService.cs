using BoB.BoBConfiguration.BaseEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.CacheManager
{
    public interface ICacheService
    {
        /// <summary>
        /// 获取缓存值，如果没有值就先初始化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheId">缓存ID</param>
        /// <param name="DataFunc">数据初始化函数</param>
        /// <param name="ValidateSecs">缓存时间</param>
        /// <returns></returns>
        T Get<T>(CacheTag tag, string cacheId, Func<T> DataFunc, int? ValidateSecs=null);

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheId"></param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="tag">缓存标识</param>
        /// <returns></returns>
        T Get<T>(CacheTag tag, string cacheId,T defaultValue=default);

        /// <summary>
        /// 删除某个缓存项
        /// </summary>
        /// <param name="cacheId"></param>
        /// <param name="tag">缓存标识</param>
        void ClearTheCache(CacheTag tag, string cacheId);

        /// <summary>
        /// 删除某个标签下的缓存项
        /// </summary>
        /// <param name="tag"></param>
        void ClearTagCache(CacheTag tag);

        /// <summary>
        /// 删除所有缓存项
        /// </summary>
        void ClearAllCache();

        /// <summary>
        /// 获取所有的缓存数据
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Object> GetAllCache();

    }
}
