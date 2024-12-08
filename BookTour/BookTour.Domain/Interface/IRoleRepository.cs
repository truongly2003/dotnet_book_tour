using BookTour.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Domain.Interface
{
    public interface IRoleRepository
    {
        Task<Role> findById(int id);
        Task<List<Role>> getAllRoleAsync();
        Task<string> FindRoleNameByIdAsync(int roleId);
    }
}
