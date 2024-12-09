using BookTour.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Domain.Interface
{
    public interface ICustomerRepository
    {
        Task<Customer> FindByCustomerIdAsync(int customerId);
        Task<Customer> saveCustomer(Customer customer);
        Task<Customer> FindCustomerByUserIdAsync(int UserId);
        Task<bool> UpdateCustomerByUserIdAsync(int UserId ,Customer customer);
        Task<Customer> SaveAsync(Customer customer);
         Task<List<Customer>> getListCustomerAsync();
    }
}
