using Nhom7_webTourdulich.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nhom7_webTourdulich.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync(); // Lấy tất cả người dùng
        Task<User> GetByEmailAsync(string email); // Lấy người dùng theo email
        Task<User> GetByResetTokenAsync(string token); // Lấy người dùng theo token đặt lại mật khẩu
        Task<User> GetByIdAsync(int id); // Lấy người dùng theo ID
        Task AddAsync(User user); // Thêm người dùng mới
        Task UpdateAsync(User user); // Cập nhật thông tin người dùng
        Task DeleteAsync(int id); // Xóa người dùng theo ID
        Task<User> GetByUsernameAndPasswordAsync(string username, string password); // Lấy người dùng theo tên đăng nhập và mật khẩu
        Task<int> SaveChangesAsync(); // Thêm phương thức này

    }
}
