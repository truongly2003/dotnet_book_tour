using BookTour.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess
{
    public class BookTourDbContext : DbContext
    {
        public BookTourDbContext(DbContextOptions<BookTourDbContext> options) : base(options)
        {
        }

        // Các DbSet cho các entity
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Customer> Customers { get; set; }

        // Bạn có thể override phương thức OnModelCreating nếu cần thiết để cấu hình thêm
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Các cấu hình thêm về entity sẽ được đặt tại đây (nếu cần)
        }
    }
}
   
