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
        public DbSet<Arrival> Arrivals { get; set; }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<Detailroute> Detailroutes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Leg> Legs { get; set; }
        public DbSet<Objects> Objects { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Paymentstatus> Paymentstatus { get; set; }
        public DbSet<Permission> Permissions { get; set; }
       
        public DbSet<Roleoperation> Roleoperations { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Statusroleoperation> Statusroleoperation { get; set; }
        public DbSet<Ticket> Tickets { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ticket>()
         .HasKey(t => new { t.BookingId, t.PassengerId });
        }
    }
}
   
