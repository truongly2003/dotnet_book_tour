using BookTour.Application.Dto;
using BookTour.Application.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Interface
{
    public interface IDecentralizationService
    {
        public Task<DecentralizationDTO> GetRoleAndPermissionsByUserIdAsync(int userId);
        public Task<DecentralizationDTO> GetListPermissionByRoleIdAsync(int roleId);
        public Task<List<PermissionDTO>> GetPermissionsByRoleIdAsync(int roleId);
        public Task UpdatePermissionsAsync(int roleId, List<PermissionOperationsRequest> permissions);

        public Task<List<PermissionDTO>> GetListPermission();
        Task AddPermissionsForRoleAsync(int roleId, List<PermissionOperationsRequest> permissions);
    }
}
