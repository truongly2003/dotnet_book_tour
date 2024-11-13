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
    public class EmployeeRepository:IEmployeeRepository
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
    }
}
