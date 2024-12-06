using MakeADishApi.Core.Interfaces;
using MakeADishApi.Core.Models;
using MakeADishApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MakeADishApi.Infrastructure.Repository;

public class OrderRepository : IOrderRepository
{

    private readonly MakeADishContext _context;

    public OrderRepository(MakeADishContext context)
    {
        _context = context;
    }
    public async Task<bool> AddOrderAsync(Order order)
    {
        //add result code, check errors, and test/fix
        await _context.Orders.AddAsync(order);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteOrderAsync(int orderid)
    {
        //add result code, check errors, and test/fix
        Order order = await _context.Orders.FindAsync(orderid);
        
        if(order != null)
        {
            _context.Remove(order);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsyc()
    {
        //add result code, check errors, and test/fix
        return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ToListAsync();
    }

    public async Task<Order> GetOrderByCustomerAsync(Customer customer)
    {
        //add result code, check errors, and test/fix
         return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync( o => o.CustomerFK == customer.CustomerID );
    }

    public async Task<IEnumerable<Order>> GetOrderByOrderDishAsync(Order.OrderDish orderdish)
    {
        //add result code, check errors, and test/fix
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderDetails)
            .Where(o => o.Dish == orderdish)
            .ToListAsync();
    }

    public async Task<Order> GetOrderByOrderIdAsync(int orderid)
    {
        //add result code, check errors, and test/fix
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.OrderID == orderid);
    }

    public async Task UpdateOrderAsync(Order order)
    {
        //add result code, check errors, and test/fix
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
}