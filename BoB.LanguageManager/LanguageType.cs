using ExtendAndHelper.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BoB.LanguageManager
{
    public enum LanguageType
    {
        [Tag("中文")]
        zh_cn,
        [Tag("English")]
        en,
    }
}
