using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Nhom7_webTourdulich.Controllers
{
    [Route("[controller]")]
    public class aboutController : Controller
    {
        private readonly ILogger<aboutController> _logger;

        public aboutController(ILogger<aboutController> logger)
        {
            _logger = logger;
        }
  [HttpGet("")]
        public IActionResult About()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}