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

        public async Task<User> findByUsername(string username)
        {
          return await _context.Users
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();
        }

    }
}
