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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly BookTourDbContext _context;

        public EmployeeRepository(BookTourDbContext context)
        {
            _context = context;
        }
        public async Task<Employee> saveEmployee(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Employees.CountAsync();
        }

        public async Task<List<Employee>> getListEmployeeAsync()
        {
            try
            {
                // Lấy danh sách tất cả nhân viên từ cơ sở dữ liệu
                var data = await _context.Employees.ToListAsync();

                // In ra dữ liệu
                Console.WriteLine("Data fetched from database:");
                foreach (var employee in data)
                {
                    Console.WriteLine($"UserId: {employee.UserId}, EmployeeId: {employee.EmployeeId}, Email: {employee.EmployeeEmail}");
                }

                return data;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu có
                Console.WriteLine($"Error in GetListEmployeeAsync: {ex.Message}");
                throw new Exception("Error fetching employee list", ex);
            }
        }

    }
}
