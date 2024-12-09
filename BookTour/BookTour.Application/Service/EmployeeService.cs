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
        public async Task<Page<EmployeeDTO>> GetAllUserAsync(int page, int size)
        {
            // Lấy tất cả nhân viên từ repository
            var data = await _employeeRepository.getListEmployeeAsync();

            // Chuyển đổi dữ liệu nhân viên thành dạng DTO
            var employeeDTO = data.Select(employee => new EmployeeDTO
            {
                employeeId = employee.EmployeeId,
                employeeEmail = employee.EmployeeEmail,
                userId = employee.UserId ?? 0,
            })
            .Skip((page - 1) * size)  // Phân trang: Bắt đầu từ phần tử thứ (page - 1) * size
            .Take(size)  // Lấy số lượng bản ghi tương ứng với kích thước trang
            .ToList();

            // Tính tổng số bản ghi
            var totalElement = data.Count();
            var totalPage = (int)Math.Ceiling((double)totalElement / size);

            // Tạo đối tượng chứa kết quả phân trang
            var result = new Page<EmployeeDTO>
            {
                Data = employeeDTO,
                TotalElement = totalElement,
                TotalPages = totalPage
            };

            return result;
        }


    }
}
