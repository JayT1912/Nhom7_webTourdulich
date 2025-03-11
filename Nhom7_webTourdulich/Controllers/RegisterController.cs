using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nhom7_webTourdulich.Models;
using Microsoft.EntityFrameworkCore;

namespace Nhom7_webTourdulich.Controllers
{
    public class RegisterController : Controller
    {
        private readonly QuanLyTourContext _quanLyTour;  // Dùng DbContext để tương tác với cơ sở dữ liệu
        private readonly ILogger<RegisterController> _logger;

        // Inject QuanLyTourContext vào constructor
        public RegisterController(ILogger<RegisterController> logger, QuanLyTourContext quanLyTour)
        {
            _logger = logger;
            _quanLyTour = quanLyTour;
        }

        // GET: Register
        public IActionResult Index()
        {
            return View(); 
        }

    // POST: Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(Register register)
    {
        if (ModelState.IsValid)
        {
            // Tạo đối tượng User từ thông tin đăng ký
            var newUser = new User
            {
                Username = register.Username,
                Password = register.Password // Cần mã hóa mật khẩu nếu có
            };

            // Lưu thông tin vào cơ sở dữ liệu bằng DbContext
            _quanLyTour.Users.Add(newUser); // Đúng
            _quanLyTour.Registers.Add(register);  // Registers là DbSet trong QuanLyTourContext
            await _quanLyTour.SaveChangesAsync(); // Lưu dữ liệu vào database

            // Chuyển hướng đến trang chính hoặc đăng nhập
            return RedirectToAction("Index", "Home");
        }

        return View(register); // Nếu Model không hợp lệ, quay lại form đăng ký
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
