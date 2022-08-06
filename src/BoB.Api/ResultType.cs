using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BoB.Api
{
    public enum ResultType
    {
        [Display(Name="值类型")]
        Value=10, // 返回的为值类型s
        [Display(Name = "对象")]
        Object =20,// 返回的为对象类型
        [Display(Name = "列表")]
        List =30, // 返回的为列表类型
        [Display(Name = "分页")]
        PageList =40, // 返回的为分页列表类型
        [Display(Name = "重定向")]
        Redirect =300,// 返回结果为重定向
        [Display(Name = "重定向到URL")]
        RedirectUrl = 301,// 返回的一个重定向的URL
        [Display(Name = "重定向到流程步骤列表页面")]
        RedirectProcessList = 302,
        [Display(Name = "重定向到流程步骤行为")]
        RedirectProcessStepAction = 303, // 一般都是直接创建的行为 
        [Display(Name = "重定向到未登录")]
        RedirectLogout = 304,
        [Display(Name = "重定向到系统初始化页面")]
        RedirectInitSystem = 305,// 返回的一个重定向的URL
        [Display(Name = "错误")]
        Error =400,
    }
}
