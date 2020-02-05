using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BoB.BoBLogger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BoB.OAuth.Pages
{
    public class IndexModel : PageModel
    {
        private IBoBLogService _log;

        public IndexModel(IBoBLogService log)
        {
            _log = log; 
        }

        //public void OnGet()
        //{

        //}

        public string Message { get; set; }
        public void OnGet()
        {
            Message = "Get used";
        }
        public void OnPost()
        {
            Message = "Post used";
        }


        //public IActionResult OnPost()
        //{
        //    _log.Error("Gsdge", "sdg", "gdsger");
        //    return Content("something");
        //}


        

        //不可以同时使用多个OnGet重载，就算参数不同也不行
        //An unhandled exception occurred while processing the request.
        //InvalidOperationException: Multiple handlers matched.The following handlers matched route data and had all constraints satisfied:
        //Microsoft.AspNetCore.Mvc.IActionResult OnGet(), Void OnGet(System.String)
        //Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.DefaultPageHandlerMethodSelector.Select(PageContext context)
        //public void OnGet(string id)
        //{

        //}
    }
}
