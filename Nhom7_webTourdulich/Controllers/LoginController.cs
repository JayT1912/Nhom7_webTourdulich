using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nhom7_webTourdulich.Models;
using Microsoft.EntityFrameworkCore;

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

// POST: Login
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Index(User user)
{
    if (ModelState.IsValid)
    {
        // Tìm người dùng trong bảng Users
        var User = await _quanLyTour.Users
            .FirstOrDefaultAsync(u => u.Username == user.Username);
        // Kiểm tra xem user có tồn tại không và password có khớp không
        if (User != null && User.Password == user.Password) 
        {
            // Đăng nhập thành công - có thể thiết lập session hoặc cookies
            HttpContext.Session.SetString("Username", User.Username); // Lưu vào session
            return RedirectToAction("Index", "Home"); // Chuyển hướng sau khi đăng nhập thành công
        }
        else
        {
            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
        }
    }

    return View(user); // Nếu đăng nhập thất bại, quay lại trang login
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