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

        public async Task<Customer> FindByCustomerIdAsync(int customerId)
        {
            // Sử dụng FindAsync để tìm kiếm theo khóa chính
            var customer = await _context.Customers
                                          .Include(c => c.User)  // Nếu muốn lấy thông tin User đi kèm với Customer
                                          .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            return customer; 
        }
    }
}
