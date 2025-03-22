using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nhom7_webTourdulich.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Nhom7_webTourdulich.Controllers;

public class LoginController : Controller
{
    private readonly QuanLyTourContext _quanLyTour;


    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger, QuanLyTourContext quanLyTour)
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
    public async Task<IActionResult> Index(User user)
    {
        if (ModelState.IsValid)
        {
            var User = await _quanLyTour.Users
                .FirstOrDefaultAsync(u => u.Username == user.Username);
            if (User != null && User.Password == user.Password) 
            {
                HttpContext.Session.SetString("Username", User.Username);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            }
        }

        return View(user); 
    }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
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