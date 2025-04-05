using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nhom7_webTourdulich.Models
{
    public class ResetPassword
    {
    public string Token { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
    public string Email { get; set; }
    }
}