using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BoB.LanguageManager
{
    public class LangService : ILangService
    {
        public string TD(string tag)
        {
            Debug.WriteLine("LangService TD");
            return "TD";
        }

        public string TL(string tag, LanguageType? languageType = null)
        {
            Debug.WriteLine("LangService TL");
            return "TL";
        }
    }
}
