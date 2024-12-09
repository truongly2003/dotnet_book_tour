using BookTour.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Domain.Interface
{
    public interface IEmployeeRepository
    {
        Task<Employee> saveEmployee(Employee employee);
        Task<int> CountAsync();
        Task<List<Employee>> getListEmployeeAsync();
    }
}
