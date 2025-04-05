using Nhom7_webTourdulich.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nhom7_webTourdulich.Repositories
{
    public class EFUserRepository : IUserRepository
    {
        private readonly QuanLyTourContext _context;

        public EFUserRepository(QuanLyTourContext context)
        {
            _context = context;
        }

        // Lấy tất cả người dùng
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Lấy người dùng theo Id
        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        // Thêm người dùng
        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // Cập nhật người dùng
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);  // Cập nhật user vào cơ sở dữ liệu
            await _context.SaveChangesAsync();  // Lưu thay đổi vào cơ sở dữ liệu
        }

        // Xóa người dùng
        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        // Truy vấn người dùng theo username và password
        public async Task<User> GetByUsernameAndPasswordAsync(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        // Thêm phương thức GetByEmailAsync
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Thêm phương thức GetByResetTokenAsync
public async Task<User> GetByResetTokenAsync(string token)
{
    return await _context.Users.FirstOrDefaultAsync(u => u.ResetToken == token);
}

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    }
}
