using BookTour.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Domain.Interface
{
    public interface IDecentralizationRepository
    {
        Task<List<string>> GetListPermissionByRoleIdAsync(int roleId);
        Task<List<string>> GetRoleAndPermissionsByUserIdAsync(int userId);

        Task<List<Permission>> GetPermissionsByRoleIdAsync(int roleId);
        Task DeleteByRoleIdAsync(int roleId);

        Task AddRoleOperationAsync(Roleoperation roleOperation);

        Task<List<Permission>> GetListPermissionAsync();
        Task AddPermissionWithOperations(int roleId, int permissionId, int operationId, int status);
        Task<bool> ExistsByRoleIdAndPermissionIdAndOperationIdAsync(int roleId, int permissionId, int operationId);
    }
}
