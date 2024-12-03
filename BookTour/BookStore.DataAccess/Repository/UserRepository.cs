using Azure;
using BookTour.Domain.Entity;
using BookTour.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
namespace BookStore.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BookTourDbContext _context;

        public UserRepository(BookTourDbContext context)
        {
            _context = context;
        }

        public async Task<User> checkUserExist(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            // Tìm user theo email
            var user = await _context.Users
                .Include(u => u.Role) // Bao gồm thông tin Role nếu cần
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            // Trả về user (null nếu không tìm thấy)
            return user;
        }


        public async Task<bool> existsByUsernameAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<List<User>> FindAllByStatusAsync(int status)
        {
            var query = await _context.Users
                .Include(u => u.Role) // Bao gồm thông tin từ bảng Role
                .Where(u => u.Status == status)
                .ToListAsync();

            Console.WriteLine("query: " + query);

            return query;
        }

        public async Task<User> findByUsername(string username)
        {
            Console.WriteLine("repo: " + username);  // Hiển thị thông tin username cho kiểm tra
            var user = await _context.Users
          .Include(u => u.Role)
          .Where(u => u.Username.ToLower() == username.ToLower())
          .FirstOrDefaultAsync();

            if (user == null)
            {
                Console.WriteLine("User not found");
            }

            return user;  // Trả về user, có thể là null nếu không tìm thấy
        }

        public async Task<User> saveUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }

}
