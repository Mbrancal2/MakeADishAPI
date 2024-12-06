
using MakeADishApi.Core.Interfaces;
using MakeADishApi.Core.Models;
using MakeADishApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MakeADishApi.Infrastructure.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly MakeADishContext _context;
    
    public CustomerRepository(MakeADishContext context)
    {
        _context = context;
    }
    public async Task<bool> AddCustomerAsync(Customer customer)
    {
        //add result code, check errors, and test/fix
        await _context.Customers.AddAsync(customer);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteCustomerAsync(int customerid)
    {
        //add result code, check errors, and test/fix
        Customer customer = await _context.Customers.FindAsync(customerid);
        if(customer != null)
        {
            _context.Remove(customer);
            return await _context.SaveChangesAsync() > 0;
        }
        return false; //customer not found
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        //add result code, check errors, and test/fix
        return await _context.Customers
            .Include(o => o.Orders)
            .ToListAsync();
    }

    // public async Task<Customer> GetCustomerAsync(Customer customer)
    // {
    //     //add result code, check errors, and test/fix
    //     return await _context.Customers
    //         .Include(o => o.Orders)
    //         .FirstOrDefaultAsync( c => c.CustomerID == customer.CustomerID );
    //}

    public async Task<Customer> GetCustomerAsync(int customerid)
    {
        return await _context.Customers
            .Include(o => o.Orders)
            .FirstOrDefaultAsync(o => o.CustomerID == customerid);
    }

    public async Task<bool> UpdateCustomerAsync(Customer customer)
    {
        //add result code, check errors, and test/fix
        _context.Customers.Update(customer);
        return await _context.SaveChangesAsync() > 0;
    }
}