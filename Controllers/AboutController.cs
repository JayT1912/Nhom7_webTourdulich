using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nhom7_webTourdulich.Models;

namespace Nhom7_webTourdulich.Controllers
{
    [Route("[controller]")]
    public class AboutController : Controller
    {
        private readonly ILogger<AboutController> _logger;
        private readonly QuanLyTourContext _context;

        public AboutController(ILogger<AboutController> logger, QuanLyTourContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
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