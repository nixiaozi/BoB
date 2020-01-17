using System;

namespace BoB.Api
{
    public static class Api
    {
        public static ApiResult<K> DoApi<T, K>(T Data, Func<T, ApiResult<K>> DoSomething)
            where T : IApiInput where K : IApiInput, IApiResult
        {
            ApiResult<K> result = new ApiResult<K>(false);
            result = DoSomething(Data);
            return result;
        }


        //可以通过嵌套Api和链式调用，对多表插值查询的情况进行适配


    }
}
