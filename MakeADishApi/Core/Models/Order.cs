using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeADishApi.Core.Models;


public class Order
{
    public enum OrderDish
    {
        Burrito,
        Burrito_Bowl,
        Quesadilla,
        Salad,
        Tacos
    }
    [Key] 
    public int OrderID {get; set;}
    //make this a foreign key
    public OrderDish Dish {get; set;}
    public DateTime OrderTime {get; set;}
    
    [ForeignKey("Customer")]
    public int CustomerFK {get; set;}
    public Customer Customer {get; set;}

    //public int OrderDetailFK{get; set;}
    public ICollection<OrderDetail> OrderDetails {get; set;}
}