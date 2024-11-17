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
    }
}
