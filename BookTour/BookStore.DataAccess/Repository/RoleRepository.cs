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

        public async Task<List<Role>> getAllRoleAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<string> FindRoleNameByIdAsync(int roleId)
        {
            // Truy vấn roleName dựa trên roleId
            var roleName = await _context.Roles
                                          .Where(r => r.RoleId == roleId)
                                          .Select(r => r.RoleName)
                                          .SingleOrDefaultAsync();  // Thay vì FirstOrDefaultAsync() vì roleId là duy nhất

            return roleName;
        }

    }

}
