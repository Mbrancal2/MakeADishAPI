using MakeADishApi.Core.Interfaces;
using MakeADishApi.Core.Models;
using MakeADishApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MakeADishApi.Infrastructure.Repository;

public class OrderDetailRepository : IOrderDetailRepository
{

    private readonly MakeADishContext _context;

    public OrderDetailRepository(MakeADishContext context)
    {
        _context = context;
    }
    public async Task AddOrderDetailAsync(OrderDetail orderdetail)
    {
        //add result code, check errors, and test/fix
        await _context.OrderDetails.AddAsync(orderdetail);
        await _context.SaveChangesAsync();
        
    }

    public async Task DeleteOrderDetailAsync(int orderdetailid)
    {
        OrderDetail orderdetail = await _context.OrderDetails.FindAsync(orderdetailid);

        if(orderdetail != null)
        {
            _context.OrderDetails.Remove(orderdetail);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync()
    {
        //add result code, check errors, and test/fix
        return await _context.OrderDetails
            .Include(o => o.Ingredient)
            .Include(o => o.Order)
            .ToListAsync();
    }

    public async Task<OrderDetail> GetOrderDetailByIdAsync(int orderdetailid)
    {
        //add result code, check errors, and test/fix
        return await _context.OrderDetails
            .Include(o => o.Ingredient)
            .Include(o => o.Order)
            .FirstOrDefaultAsync(o => o.OrderDetailID == orderdetailid);
    }

    public async Task<IEnumerable<OrderDetail>> GetOrderDetailByIngredientAsync(Ingredient ingredient)
    {
        //add result code, check errors, and test/fix
        return await _context.OrderDetails
            .Include(o => o.Ingredient)
            .Include(o => o.Order)
            .Where(o => o.Ingredient == ingredient)
            .ToListAsync();
    }

    public async Task<IEnumerable<OrderDetail>> GetOrderDetailByOrderAsync(Order order)
    {
        //add result code, check errors, and test/fix
         return await _context.OrderDetails
            .Include(o => o.Ingredient)
            .Include(o => o.Order)
            .Where(o => o.Order == order)
            .ToListAsync();

    }

    public async Task UpdateOrderDetailAsync(OrderDetail orderdetail)
    {
        //add result code, check errors, and test/fix
        _context.OrderDetails.Update(orderdetail);
        await _context.SaveChangesAsync();
    }
}