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

        public async Task<List<User>> findAllUser()
        {
            return await _context.Users.ToListAsync();
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


    }

}
