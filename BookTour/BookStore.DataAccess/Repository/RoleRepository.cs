using BookTour.Domain.Entity;
using BookTour.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BookTourDbContext _context;

        public RoleRepository(BookTourDbContext context)
        {
            _context = context;
        }

        public async Task<Role> findById(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == id);
        }
    }

}
