using BookStore.DataAccess.Repository;
using BookTour.Application.Dto;
using BookTour.Application.Interface;
using BookTour.Domain.Entity;
using BookTour.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CustomerDTO> FindCustomerByUserIdAsync(int UserId)
        {
            var data=await _customerRepository.FindCustomerByUserIdAsync(UserId);
            var customerDTO=new CustomerDTO
            {
                userId=data.UserId,
                name=data.CustomerName,
                address=data.CustomerAddress,
                email=data.CustomerEmail,
                 phone=data.CustomerPhone,
            };
            return customerDTO;
        }

        public async Task<bool> UpdateCustomerByUserIdAsync(int UserId, CustomerDTO customerDTO)
        {
            var find = await _customerRepository.FindCustomerByUserIdAsync(UserId);
            find.CustomerName = customerDTO.name ?? find.CustomerName;
            find.CustomerEmail = customerDTO.email ?? find.CustomerEmail;
            find.CustomerAddress = customerDTO.address ?? find.CustomerAddress;
            find.CustomerPhone = customerDTO.phone ?? find.CustomerPhone;

            await _customerRepository.UpdateCustomerByUserIdAsync(UserId,find);
            return true;
        }

        public async Task<Page<CustomerDTO>> GetAllUserAsync(int page, int size)
        {
            // L?y t?t c? khách hàng t? repository
            var data = await _customerRepository.getListCustomerAsync();

            // Chuy?n ??i d? li?u thành d?ng DTO
            var userDTO = data.Select(user => new CustomerDTO
            {
                userId = user.UserId, // ??m b?o là 'user' ch? không ph?i 'data'
                name = user.CustomerName,
                address = user.CustomerAddress,
                email = user.CustomerEmail,
                phone = user.CustomerPhone,
            })
            .Skip((page - 1) * size)  // Phân trang
            .Take(size)  // L?y s? l??ng theo trang
            .ToList();

            // Tính t?ng s? khách hàng
            var totalElement = data.Count();
            var totalPage = (int)Math.Ceiling((double)totalElement / size);

            // Tr? v? ??i t??ng ch?a d? li?u phân trang
            var result = new Page<CustomerDTO>
            {
                Data = userDTO,
                TotalElement = totalElement,
                TotalPages = totalPage
            };

            return result;
        }

    }
}
