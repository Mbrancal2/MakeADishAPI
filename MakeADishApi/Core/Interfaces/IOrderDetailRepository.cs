using MakeADishApi.Core.Models;

namespace MakeADishApi.Core.Interfaces;

public interface IOrderDetailRepository
{
    Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync();
    Task<OrderDetail> GetOrderDetailByIdAsync(int orderdetailid);
    Task<IEnumerable<OrderDetail>> GetOrderDetailByIngredientAsync(Ingredient ingredient);
    Task<IEnumerable<OrderDetail>> GetOrderDetailByOrderAsync (Order order);
    Task UpdateOrderDetailAsync(OrderDetail orderdetail);
    Task DeleteOrderDetailAsync (int orderdetailid);
    Task AddOrderDetailAsync (OrderDetail orderdetail);
}

    
