using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nhom7_webTourdulich.Repositories
{
    public interface IEmailService
    {
                Task SendResetPasswordEmail(string email, string resetLink);

    }
}