using BookStore.DataAccess.Repository;
using BookTour.Application.Dto;
using BookTour.Application.Dto.Request;
using BookTour.Application.Interface;
using BookTour.Domain.Entity;
using BookTour.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Service
{
    public class DecentralizationService : IDecentralizationService
    {
        private readonly IDecentralizationRepository _decentralizationRepository;
        private readonly IRoleRepository _roleRepository;
        public DecentralizationService(IDecentralizationRepository decentralizationRepository, IRoleRepository roleRepository)  
        {   
            _decentralizationRepository = decentralizationRepository;
            _roleRepository = roleRepository;
        }
        public async Task<DecentralizationDTO> GetListPermissionByRoleIdAsync(int roleId)
        {
            // Lấy danh sách permission từ decentralizationRepository
            var permissions = await _decentralizationRepository.GetListPermissionByRoleIdAsync(roleId);

            // Lấy tên role từ roleRepository
            var roleName = await _roleRepository.FindRoleNameByIdAsync(roleId);

            if (roleName == null)
            {
                throw new Exception("Role not found");
            }

            // Tạo đối tượng response
            var response = new DecentralizationDTO
            {
                RoleId = roleId,
                RoleName = roleName,
                Permissions = permissions
            };

            return response;
        }


        public async Task<DecentralizationDTO> GetRoleAndPermissionsByUserIdAsync(int userId)
        {
            Console.WriteLine($"Received userId in service: {userId}");


            // Get the list of roles and permissions for the given userId from the repository
            var results = await _decentralizationRepository.GetRoleAndPermissionsByUserIdAsync(userId);

            var response = new DecentralizationDTO();

            // Check if results have data
            if (results.Any())
            {
                // Assuming the first item in results is the role name
                response.RoleName = results.FirstOrDefault(); // Safely accessing RoleName

                // Assign the list of permissions from the results
                response.Permissions = results.ToList();
            }

            return response;
        }


        public async Task<List<PermissionDTO>> GetPermissionsByRoleIdAsync(int roleId)
        {
            // Lấy các permission cho roleId từ repository
            List<Permission> permissions = await _decentralizationRepository.GetPermissionsByRoleIdAsync(roleId);

            if (permissions == null || permissions.Count == 0)
            {
                return new List<PermissionDTO>(); // Trả về danh sách rỗng nếu không có permission
            }

            // Chuyển đổi danh sách Permission thành PermissionDTO
            var permissionResponses = permissions.Select(permission => new PermissionDTO
            {
                Id = permission.PermissionId,        // Mapping PermissionId
                PermissionName = permission.PermissionName // Mapping PermissionName
            }).ToList();

            return permissionResponses; // Trả về danh sách PermissionDTO
        }


        public async Task UpdatePermissionsAsync(int roleId, List<PermissionOperationsRequest> permissions)
        {
            try
            {
                // Xoá các role operation hiện tại theo roleId
                await _decentralizationRepository.DeleteByRoleIdAsync(roleId);

                // Lặp qua từng permission và các operation ids
                foreach (var permission in permissions)
                {
                    int permissionId = permission.permissionId;
                    List<int> operationIds = permission.operationIds;

                    if (operationIds != null && operationIds.Any())
                    {
                        foreach (var operationId in operationIds)
                        {
                            // Tạo một đối tượng Roleoperation mới để thêm vào cơ sở dữ liệu
                            var roleOperation = new Roleoperation
                            {
                                RoleId = roleId,
                                PermissionId = permissionId,
                                OperationId = 1,
                                StatusId = 1 // Giả sử StatusId = 1 là trạng thái "hoạt động"
                            };

                            // Thêm permission với operation vào cơ sở dữ liệu
                            await _decentralizationRepository.AddRoleOperationAsync(roleOperation);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (throw hoặc ghi log nếu cần)
                throw new Exception("An error occurred while updating permissions.", ex);
            }
        }

        public async Task<List<PermissionDTO>> GetListPermission()
        {
            var permissions = await _decentralizationRepository.GetListPermissionAsync();

            // Chuyển đổi danh sách permission thành PermissionDTO
            var permissionDTOs = permissions.ConvertAll(permission => new PermissionDTO
            {
                Id = permission.PermissionId,
                PermissionName = permission.PermissionName
            });

            return permissionDTOs;
        }


        public async Task AddPermissionsForRoleAsync(int roleId, List<PermissionOperationsRequest> permissions)
        {
            try
            {
                foreach (var permission in permissions)
                {
                    int permissionId = permission.permissionId;
                    List<int> operationIds = permission.operationIds;

                    // Kiểm tra nếu operationIds không null và có phần tử hợp lệ
                    if (operationIds != null && operationIds.Any())
                    {
                        foreach (var operationId in operationIds)
                        {
                            // Kiểm tra xem quyền và thao tác đã tồn tại chưa, nếu chưa thì thêm vào DB
                            var exists = await _decentralizationRepository.ExistsByRoleIdAndPermissionIdAndOperationIdAsync(roleId, permissionId, operationId);

                            if (!exists)
                            {
                                // Tạo một đối tượng mới để thêm vào cơ sở dữ liệu
                                var roleOperation = new Roleoperation
                                {
                                    RoleId = roleId,
                                    PermissionId = permissionId,
                                    OperationId = operationId, // Sử dụng đúng operationId từ request
                                    StatusId = 1 // Giả sử 1 là trạng thái "hoạt động"
                                };

                                // Thêm permission với operation vào cơ sở dữ liệu
                                await _decentralizationRepository.AddRoleOperationAsync(roleOperation);
                            }
                        }
                    }
                    else
                    {
                        // Nếu operationIds là rỗng hoặc null, có thể log lại hoặc xử lý theo yêu cầu
                        Console.WriteLine($"Permission {permissionId} has no valid operationIds, skipping.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log lỗi chi tiết
                Console.WriteLine($"Error in AddPermissionsForRoleAsync: {ex.Message}");
                throw new Exception("Error adding permissions for role", ex);
            }
        }

    }


}
