using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ACM.BaseAutoAction
{
    public enum TaskExecuteStatusEnum
    {
        [Display(Name="未执行")]
        UnDo=0,
        [Display(Name = "执行中")]
        Executing = 10,
        [Display(Name = "执行失败")]
        Fail =40,
        [Display(Name = "执行完成")]
        Completed = 100,
        [Display(Name = "系统终止")]
        SystemClosure = 101,



    }
}
