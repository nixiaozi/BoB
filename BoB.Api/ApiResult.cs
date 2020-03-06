using System;

namespace BoB.Api
{
    public class ApiResult<R> : IApiResult
    {
        /// <summary>
        /// 构造函数初始化
        /// </summary>
        /// <param name="success">成功还是失败</param>
        public ApiResult(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// 构造函数初始化
        /// </summary>
        /// <param name="success">操作成功还是失败</param>
        /// <param name="controlFuncDefine">控制器预定义方法</param>
        public ApiResult(bool success, string controlFuncDefine)
        {
            Success = success;
            ControlFuncDefine = controlFuncDefine;
        }

        public string ControlFuncDefine { get; set; }

        public Guid Opreator { get; set; }

        /// <summary>
        /// 请求成功还是失败
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 数据为数组时的长度
        /// </summary>
        public int DataCount { get; set; }

        /// <summary>
        /// 返回的数据类型为K
        /// </summary>
        public R Data { get; set; }

        /// <summary>
        /// 数据有分页显示分页的大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 数据有分页显示当前分页数
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 显示给客户端的信息
        /// </summary>
        public string DisplayMessage { get; set; }

        /// <summary>
        /// 下一跳跳转链接地址
        /// </summary>
        public string RedirectUrl { get; set; }


        public ApiResult<R> DoApi(Func<ApiResult<R>, ApiResult<R>> DoSomeThing)
        {
            return DoSomeThing.Invoke(this);
        }


        //public ApiResult<T> MapTo<T>()
        //{
        //    var mapperService = BoBContainer.ServiceProvider.GetService<IAutoMapperService>();

        //    var result=new ApiResult<T>
        //    {
        //        a
        //    }

        //    mapperService.DoMap<R, T>(this.Data);

        //}


    }
}
