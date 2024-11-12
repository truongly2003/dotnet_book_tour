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




    }
}
