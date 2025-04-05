using Nhom7_webTourdulich.Models;
using Nhom7_webTourdulich.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Nhom7_webTourdulich.Controllers
{
    public class PasswordController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService; // Đảm bảo khai báo _emailService

        // Constructor to inject dependencies
        public PasswordController(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService; // Khởi tạo _emailService qua constructor
        }

        // GET: /Password/ForgotPassword
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // POST: /Password/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra nếu email đã tồn tại trong hệ thống
                var user = await _userRepository.GetByEmailAsync(model.Email);

                if (user != null)
                {
                    // Tạo token ngẫu nhiên cho liên kết reset mật khẩu
                    var resetToken = Guid.NewGuid().ToString();

                    // Lưu token vào session tạm thời
                    HttpContext.Session.SetString("ResetToken", resetToken);

                    // Cập nhật token trong cơ sở dữ liệu của người dùng
                    user.ResetToken = resetToken;
                    await _userRepository.UpdateAsync(user);

                    // Tạo liên kết reset mật khẩu
                    var resetLink = Url.Action("ResetPassword", "Password", new { token = resetToken }, Request.Scheme);

                    // Gửi email cho người dùng với liên kết reset mật khẩu
                    await _emailService.SendResetPasswordEmail(user.Email, resetLink);

                    // Thông báo người dùng rằng liên kết reset mật khẩu đã được gửi
                    TempData["SuccessMessage"] = "A password reset link has been sent to your email.";

                    return RedirectToAction("ResetPassword", new { token = resetToken });
                }
                else
                {
                    ModelState.AddModelError("", "Email not found in the system.");
                }
            }

            return View(model);
        }

        // GET: /Password/ResetPassword
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "The link is invalid or has expired.";
                return RedirectToAction("ForgotPassword", "Password");
            }

            // Kiểm tra token trong cơ sở dữ liệu
            var user = await _userRepository.GetByResetTokenAsync(token);
            if (user == null)
            {
                TempData["ErrorMessage"] = "The link is invalid or has expired.";
                return RedirectToAction("ForgotPassword", "Password");
            }

            // Nếu token hợp lệ, hiển thị trang reset mật khẩu
            ViewData["Token"] = token;
            return View();
        }

        // POST: /Password/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {
            if (ModelState.IsValid)
            {
                // Lấy token từ session
                var storedToken = HttpContext.Session.GetString("ResetToken");

                if (storedToken != null && storedToken == model.Token)
                {
                    // Lấy người dùng từ email (sử dụng token)
                    var user = await _userRepository.GetByEmailAsync(model.Email);

                    if (user != null)
                    {
                        // Kiểm tra mật khẩu mới và mật khẩu xác nhận có khớp không
                        if (model.NewPassword != model.ConfirmNewPassword)
                        {
                            ModelState.AddModelError("", "The new password and confirmation password do not match.");
                            return View(model);
                        }

                        // Lưu mật khẩu vào cơ sở dữ liệu (chú ý: không nên lưu mật khẩu mà không mã hóa)
                        user.Password = model.NewPassword;

                        // Cập nhật mật khẩu vào cơ sở dữ liệu
                        await _userRepository.UpdateAsync(user);

                        // Lưu thay đổi vào cơ sở dữ liệu
                        await _userRepository.SaveChangesAsync();

                        // Xóa token khỏi session
                        HttpContext.Session.Remove("ResetToken");

                        TempData["SuccessMessage"] = "Your password has been successfully changed!";
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid email address.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The link is invalid or has expired.");
                }
            }

            return View(model);
        }
    }
}
