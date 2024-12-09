using BookTour.Application.Dto;
using BookTour.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Interface
{
    public interface ICustomerService
    {
        Task<CustomerDTO> FindCustomerByUserIdAsync(int UserId);
        Task<bool> UpdateCustomerByUserIdAsync(int UserId, CustomerDTO customerDTO);
         Task<Page<CustomerDTO>> GetAllUserAsync(int page, int size);
    }
}
