using Nhom7_webTourdulich.Models;
using Nhom7_webTourdulich.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nhom7_webTourdulich.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Phương thức GET cho trang đăng nhập
        public IActionResult Login()
        {
            return View();
        }

        // Phương thức POST xử lý đăng nhập
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra tên đăng nhập và mật khẩu hợp lệ
                var dbLogin = await _userRepository.GetByUsernameAndPasswordAsync(login.Username, login.Password);

                if (dbLogin != null)
                {
                    // Lưu thông tin người dùng vào Session
                    HttpContext.Session.SetString("Username", dbLogin.Username);

                    // Tạo Claims cho người dùng và thực hiện đăng nhập
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, dbLogin.Username),
                        new Claim(ClaimTypes.Role, dbLogin.Role ?? "user") // Đảm bảo Role là "admin" cho người dùng admin
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    // Đăng nhập người dùng
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    // Kiểm tra vai trò và chuyển hướng
                    if (dbLogin.Role == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    // Thêm lỗi nếu tên đăng nhập hoặc mật khẩu không đúng
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }

            return View(login);
        }



        

        // Phương thức xử lý đăng xuất
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Đăng xuất và xóa cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
