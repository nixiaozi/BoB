using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoB.RazorTest
{
    public class HeaderModel : PageModel   //类别名不能重名，会有重名提醒
    {
        public void OnGet()
        {

        }
    }
}