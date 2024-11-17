using BookTour.Domain.Entity;
using BookTour.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookStore.DataAccess.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BookTourDbContext _context;

        public CustomerRepository(BookTourDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> FindByCustomerIdAsync(int userId)
        {
            Console.WriteLine("Finding customer with UserId: " + userId);  // Kiểm tra đầu vào userId

            // Tìm kiếm Customer dựa trên UserId
            var customer = await _context.Customers
                                          .Include(c => c.User)
                                          .FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null)
            {
                Console.WriteLine("No customer found with UserId: " + userId);  // Kiểm tra nếu không tìm thấy
            }
            else
            {
                Console.WriteLine("Customer found: " + customer.CustomerId);  // Kiểm tra kết quả nếu tìm thấy
            }

            return customer;
        }

      
        public async Task<Customer> saveCustomer(Customer customer)
        {

            Console.WriteLine($"Saving customer: {customer.CustomerId}, {customer.CustomerName}");
            try
            {
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                return customer;
            }
            catch (DbUpdateException ex)
            {
                // Ghi log chi tiết lỗi
                Console.WriteLine($"Database update error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                throw; // Ném lại lỗi để xử lý ở nơi khác nếu cần thiết
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi chung
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }


        // phần trưởng
      
        public async Task<Customer> FindCustomerByUserIdAsync(int UserId)
        {
            var query = await _context.Customers
                .Include(c => c.User)
                .Where(c => c.UserId == UserId)
                .FirstOrDefaultAsync();
            return query;
        }
        public async Task<bool> UpdateCustomerByUserIdAsync(int UserId, Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
