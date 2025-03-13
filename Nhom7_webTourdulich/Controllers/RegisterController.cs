using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nhom7_webTourdulich.Models;
using Microsoft.EntityFrameworkCore;

namespace Nhom7_webTourdulich.Controllers
{
    public class RegisterController : Controller
    {
        private readonly QuanLyTourContext _quanLyTour; 
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(ILogger<RegisterController> logger, QuanLyTourContext quanLyTour)
        {
            _logger = logger;
            _quanLyTour = quanLyTour;
        }

        public IActionResult Index()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Register register)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    Username = register.Username,
                    Password = register.Password 
                };
                _quanLyTour.Users.Add(newUser); 
                _quanLyTour.Registers.Add(register);  
                await _quanLyTour.SaveChangesAsync(); 

                return RedirectToAction("Index", "Home");
            }

            return View(register);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
