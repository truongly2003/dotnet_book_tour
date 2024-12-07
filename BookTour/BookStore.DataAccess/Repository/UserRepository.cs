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
            Console.WriteLine("emai repo :", email);
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            // Tìm user theo email
            var user = await _context.Users
                .Include(u => u.Role) // Bao gồm thông tin Role nếu cần
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            // Trả về user (null nếu không tìm thấy)
            return user;
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            // Kiểm tra xem có người dùng nào có email giống với email đã cho không
            return await _context.Users
                .AnyAsync(u => u.Email.ToLower() == email.ToLower());  // Kiểm tra bất kỳ user nào có email trùng khớp
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

        public async Task<User> FindUserById(int id)
        {
            Console.WriteLine("repo: " + id);  // Hiển thị thông tin id cho kiểm tra

            // Tìm kiếm người dùng trong cơ sở dữ liệu theo ID
            var user = await _context.Users
                .Include(u => u.Role)  // Bao gồm thông tin về Role nếu cần
                .FirstOrDefaultAsync(u => u.UserId == id);  // Tìm người dùng theo id

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

        public async Task<User> updateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null.");

            // Tìm người dùng theo UserId
            var existingUser = await _context.Users
                .Include(u => u.Role)  // Bao gồm thông tin Role nếu cần
                .FirstOrDefaultAsync(u => u.UserId == user.UserId);

            // Kiểm tra nếu người dùng không tồn tại
            if (existingUser == null)
                throw new Exception("User not found.");

            // Cập nhật các thông tin cần thiết của người dùng
            existingUser.Username = user.Username ?? existingUser.Username; // Nếu username mới null, giữ giá trị cũ
            existingUser.Email = user.Email ?? existingUser.Email;  // Nếu email mới null, giữ giá trị cũ
            existingUser.Role = user.Role ?? existingUser.Role;  // Nếu role mới null, giữ giá trị cũ

            // Lưu lại thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            // Trả về người dùng đã cập nhật
            return existingUser;
        }


    }

}
