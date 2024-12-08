using BookStore.DataAccess.Repository;
using BookTour.Application.Dto;
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
    public class EmployeeService:IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public async Task<string> generateEmployeeIdBy()
        {
            int count = await _employeeRepository.CountAsync();

            // Trả về ID nhân viên mới theo định dạng "NV_01", "NV_02", v.v.
            return string.Format("NV_{0:D2}", count + 1);
        }

       

    }
}
