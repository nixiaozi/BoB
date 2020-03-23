using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BoB.EFDbContext.Enums
{
    public enum DataStatus
    {
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 软删除，不是真正的删除，只是在数据库标记
        /// </summary>
        [Display(Name = "删除")]
        Delete = 1,
    }
}
