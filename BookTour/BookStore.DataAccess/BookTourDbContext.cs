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
        
    }
}
