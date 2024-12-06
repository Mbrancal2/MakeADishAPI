

using MakeADishApi.Core.Models;
namespace MakeADishApi.Application.Interfaces;

public interface IDbService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> GetCustomerAsync(int customerid);
    Task<(bool success, string message)> UpdateCustomerAsync(Customer customer);
    Task<(bool success, string message)> DeleteCustomerAsync(int customerid);
    Task<(bool success, string message, Customer c)> AddCustomerAsync(Customer customer);

    Task<IEnumerable<Ingredient>> GetAllIngredientsAsync();
    Task<IEnumerable<Ingredient>> GetIngredientsByCategoryAsync(Ingredient.IngredientCategory ingredientcategory);
    Task<Ingredient> GetIngredientByIdAsync(int ingredientid);
    Task<Ingredient> GetIngredientByNameAsync(string name);
    Task UpdateIngredientAsync(Ingredient ingredient);
    Task DeleteIngredientAsync(int ingredientid);
    Task AddIngredientAsync(Ingredient ingredient);

    Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync();
    Task<OrderDetail> GetOrderDetailByIdAsync(int orderdetailid);
    Task<IEnumerable<OrderDetail>> GetOrderDetailByIngredientAsync(Ingredient ingredient);
    Task<IEnumerable<OrderDetail>> GetOrderDetailByOrderAsync (Order order);
    Task UpdateOrderDetailAsync(OrderDetail orderdetail);
    Task DeleteOrderDetailAsync (int orderdetailid);
    Task AddOrderDetailAsync (OrderDetail orderdetail);

    Task<IEnumerable<Order>> GetAllOrdersAsyc();
    Task<Order> GetOrderByOrderIdAsync(int orderid);
    Task<IEnumerable<Order>> GetOrderByOrderDishAsync(Order.OrderDish orderdish);
    Task<Order> GetOrderByCustomerAsync (Customer customer);
    Task UpdateOrderAsync (Order order);
    Task<(bool success, string message)> DeleteOrderAsync (int orderid);
    Task<(bool success, string message, Order o)> AddOrderAsync (Order order);
}