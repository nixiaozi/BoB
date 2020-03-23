using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.ExtendAndHelper.CustomAttributes
{
    /// <summary>
    /// 这个是一个翻译标识
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class TagAttribute : Attribute
    {
        public TagAttribute(string tag)
        {
            _tag = tag;
        }

        private string _tag;

        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

    }
}
