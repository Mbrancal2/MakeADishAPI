using MakeADishApi.Core.Models;

namespace MakeADishApi.Core.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrdersAsyc();
    Task<Order> GetOrderByOrderIdAsync(int orderid);
    Task<IEnumerable<Order>> GetOrderByOrderDishAsync(Order.OrderDish orderdish);
    Task<Order> GetOrderByCustomerAsync (Customer customer);
    Task UpdateOrderAsync (Order order);
    Task<bool> DeleteOrderAsync (int orderid);
    Task<bool> AddOrderAsync (Order order);
}