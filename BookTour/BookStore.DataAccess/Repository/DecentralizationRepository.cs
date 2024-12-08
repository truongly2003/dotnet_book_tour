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


    public class DecentralizationRepository : IDecentralizationRepository
    {
        private readonly BookTourDbContext _dbContext;
        public DecentralizationRepository(BookTourDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<string>> GetListPermissionByRoleIdAsync(int roleId)
        {
            // Sử dụng LINQ để truy vấn danh sách permissionName
            var permissions = await _dbContext.Roleoperations
                .Where(ro => ro.RoleId == roleId) // Kiểm tra RoleId từ bảng Roleoperation
                .Select(ro => ro.Permission.PermissionName) // Chọn PermissionName từ bảng Permission
                .Distinct()
                .ToListAsync();

            return permissions;
        }

        public async Task<List<string>> GetRoleAndPermissionsByUserIdAsync(int userId)
        {
            Console.WriteLine($"userId repo : {userId}");

            var results = await (
                from u in _dbContext.Users
                join r in _dbContext.Roles on u.Role.RoleId equals r.RoleId
                join rp in _dbContext.Roleoperations on r.RoleId equals rp.Role.RoleId // Explicit join through Roleoperations collection
                join p in _dbContext.Permissions on rp.Permission.PermissionId equals p.PermissionId
                where u.UserId == userId
                select new
                {
                    RoleName = r.RoleName,
                    PermissionName = p.PermissionName
                })
                .Distinct() // Ensure distinct permission names
                .ToListAsync();

            return results.Select(r => r.PermissionName).ToList();
        }


        public async Task<List<Permission>> GetPermissionsByRoleIdAsync(int roleId)
        {
            // Lấy danh sách permissions theo roleId từ các bảng Roleoperations và Permissions
            var results = await (
                from rp in _dbContext.Roleoperations
                join p in _dbContext.Permissions on rp.PermissionId equals p.PermissionId
                where rp.RoleId == roleId // Lọc theo roleId
                select new Permission
                {
                    PermissionId = p.PermissionId, // Lấy PermissionId
                    PermissionName = p.PermissionName // Lấy PermissionName
                }
            )
            .Distinct()  // Đảm bảo danh sách trả về không bị trùng
            .ToListAsync();

            return results ?? new List<Permission>();  // Nếu không có kết quả, trả về danh sách rỗng
        }


        public async Task DeleteByRoleIdAsync(int roleId)
        {
            var roleOperations = await _dbContext.Set<Roleoperation>()
                                                .Where(ro => ro.RoleId == roleId)
                                                .ToListAsync();

            _dbContext.Set<Roleoperation>().RemoveRange(roleOperations);
            await _dbContext.SaveChangesAsync();
        }

        // Thêm một RoleOperation mới vào cơ sở dữ liệu
        public async Task AddRoleOperationAsync(Roleoperation roleOperation)
        {
            await _dbContext.Set<Roleoperation>().AddAsync(roleOperation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Permission>> GetListPermissionAsync()
        {
            return await _dbContext.Permissions.ToListAsync();
        }

        public async Task AddPermissionWithOperations(int roleId, int permissionId, int operationId, int status)
        {
            var roleOperation = new Roleoperation
            {
                RoleId = roleId,
                PermissionId = permissionId,
                OperationId = operationId,
                StatusId = status
            };

            // Add to the DbSet and save changes to the database
            _dbContext.Roleoperations.Add(roleOperation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsByRoleIdAndPermissionIdAndOperationIdAsync(int roleId, int permissionId, int operationId)
        {
            // Sử dụng LINQ để kiểm tra sự tồn tại của Roleoperation
            var exists = await _dbContext.Roleoperations
                .AnyAsync(ro => ro.RoleId == roleId && ro.PermissionId == permissionId && ro.OperationId == operationId);

            return exists;
        }

    }
}
