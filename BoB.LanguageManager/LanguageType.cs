using ExtendAndHelper.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BoB.LanguageManager
{
    public enum LanguageType
    {
        [Display(Name ="中文")]
        zh_cn,
        [Display(Name = "English")]
        en,
    }
}
