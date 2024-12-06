

using MakeADishApi.Core.Models;

namespace MakeADishApi.Core.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    //Task<Customer> GetCustomerAsync(Customer customer);
    Task<Customer> GetCustomerAsync(int customerid);
    Task<bool> UpdateCustomerAsync(Customer customer);
    Task<bool> DeleteCustomerAsync(int customerid);
    Task<bool> AddCustomerAsync(Customer customer);
}