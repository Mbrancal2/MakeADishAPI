
using MakeADishApi.Application.Interfaces;
using MakeADishApi.Core.Interfaces;
using MakeADishApi.Core.Models;

namespace MakeADishApi.Application.Services;

public class DbService : IDbService
{
    private readonly ICustomerRepository _customer_repository;
    private readonly IIngredientsRepository _Ingredient_repository;
    private readonly IOrderDetailRepository _orderdetail_repository;
    private readonly IOrderRepository _order_repository;

    public DbService(ICustomerRepository custome_repository, IIngredientsRepository ingredients_repository,
    IOrderDetailRepository order_detail_repository, IOrderRepository order_repository)
    {
        _customer_repository = custome_repository;
        _Ingredient_repository = ingredients_repository;
        _orderdetail_repository = order_detail_repository;
        _order_repository = order_repository;
    }
    //customer is at the momment a basic class. will need to add thinks like checking
    //if the user alredy exists, and store password hashes
    //make a way to change your password if you cant remember it.
    //maybe add some form of duel authentication.
    public async Task<(bool success, string message, Customer c)> AddCustomerAsync(Customer customer)
    {
        //customer alredy exsists in the database
        if(await _customer_repository.GetCustomerAsync(customer.CustomerID) != null)
        {
            return (false, "Customer alredy exists", customer);
        }

        try
        {
            bool result = await _customer_repository.AddCustomerAsync(customer);
            return result
                ? (true, "Customer added to database", customer)
                : (false, "Failed to add customer to database", customer);

        }
        catch (Exception ex)
        {
            return (false, $"Failed to add customer to databse due to an unexpected error: {ex.Message}", customer);
        }
    }

    public Task AddIngredientAsync(Ingredient ingredient)
    {
        throw new NotImplementedException();
    }
    //add test
    public async Task<(bool success, string message, Order o)> AddOrderAsync(Order order)
    {
        if(await _order_repository.GetOrderByOrderIdAsync(order.OrderID) != null)
        {
            return (false, "Order already exists", order);
        }
        try
        {
            bool result = await _order_repository.AddOrderAsync(order);
            return result
                ? (true, "Order added to the database", order)
                : (false, "Faild to add order to the database", order); 

        }
        catch(Exception ex)
        {
            return(false, $"Failed to add order to the database due to an unexpected error: {ex.Message}", order);
        }
        
    }

    public Task AddOrderDetailAsync(OrderDetail orderdetail)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool success, string message)> DeleteCustomerAsync(int customerid)
    {
        if (await _customer_repository.GetCustomerAsync(customerid) == null)
        {
            return (false, "No matching customer exists in the database");
        }
        
        try{
            bool result = await _customer_repository.DeleteCustomerAsync(customerid);
            return result
                ? (true, $"Customer with id: {customerid} deleted successfully")
                : (false, $"Failed to delete customer with id: {customerid}");
        }
        catch(Exception ex)
        {
            return (false, $"Failed to delete customer due to an unexpected error: {ex.Message}");
        }

        
    }

    public Task DeleteIngredientAsync(int ingredientid)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool success, string message)> DeleteOrderAsync(int orderid)
    {
        if (await _order_repository.GetOrderByOrderIdAsync(orderid) == null)
        {
            return (false, "No matching order exists in the database");
        }
        try
        {
            bool result = await _order_repository.DeleteOrderAsync(orderid);
            return result
                ? (true, $"Order with id: {orderid} deleted successfully")
                : (false, $"Failed to delete order with id: {orderid}");

        }
        catch(Exception ex)
        {
            return (false, $"Failed to delete order due to an unexpected error: {ex.Message}");
        }
    }

    public Task DeleteOrderDetailAsync(int orderdetailid)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        try
        {
            return await _customer_repository.GetAllCustomersAsync();
        }
        catch(Exception ex)
        {
            throw new Exception($"An error occurred while getting customers: {ex.Message}");
        }

    }

    public Task<IEnumerable<Ingredient>> GetAllIngredientsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetAllOrdersAsyc()
    {
        
    }

    public async Task<Customer> GetCustomerAsync(int customerid)
    {
        try{
            return await _customer_repository.GetCustomerAsync(customerid);
        }
        catch(Exception ex)
        {
            throw new Exception($"An error occurred while getting customer: {ex.Message}");
        }
    }

    public Task<Ingredient> GetIngredientByIdAsync(int ingredientid)
    {
        throw new NotImplementedException();
    }

    public Task<Ingredient> GetIngredientByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Ingredient>> GetIngredientsByCategoryAsync(Ingredient.IngredientCategory ingredientcategory)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetOrderByCustomerAsync(Customer customer)
    {
        
    }

    public Task<IEnumerable<Order>> GetOrderByOrderDishAsync(Order.OrderDish orderdish)
    {
        
    }

    public Task<Order> GetOrderByOrderIdAsync(int orderid)
    {
        
    }

    public Task<OrderDetail> GetOrderDetailByIdAsync(int orderdetailid)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderDetail>> GetOrderDetailByIngredientAsync(Ingredient ingredient)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderDetail>> GetOrderDetailByOrderAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public async Task<(bool success, string message)> UpdateCustomerAsync(Customer customer)
    {
        var existing_customer = await _customer_repository.GetCustomerAsync(customer.CustomerID);
        //checks if an the customer exists
        if(existing_customer == null)
        {
            return (false, "Customer not found");
        }
        //checks to see if a customer was properly deleted or not
        try {
            bool result = await _customer_repository.UpdateCustomerAsync(customer);
            return result
                ? (true, "Customer updated successfully")
                : (false, "Failed to update customer");
        }
        catch (Exception ex)
        {
            return (false, $"Failed to update customer due to an unexpected error: {ex.Message}");
        }
    }


    public Task UpdateIngredientAsync(Ingredient ingredient)
    {
        throw new NotImplementedException();
    }

    public Task UpdateOrderAsync(Order order)
    {
        
    }

    public Task UpdateOrderDetailAsync(OrderDetail orderdetail)
    {
        throw new NotImplementedException();
    }
}