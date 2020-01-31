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

        public IActionResult OnGet()
        {
            _log.Error("Gsdge", "sdg", "gdsger");
            return Content("something");
        }
    }
}
