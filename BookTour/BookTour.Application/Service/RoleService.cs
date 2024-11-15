using BookTour.Application.Dto;
using BookTour.Application.Interface;
using BookTour.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task<List<RoleDTO>> getAllRole()
        {
            var data = await _roleRepository.getAllRoleAsync();

            var roleDTO = data.Select(role => new RoleDTO
            {
                roleId = role.RoleId,
                roleName = role.RoleName
            }).ToList();
            return roleDTO;
        }
    }
}
