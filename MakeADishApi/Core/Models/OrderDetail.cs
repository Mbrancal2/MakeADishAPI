using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeADishApi.Core.Models;

public class OrderDetail
{
    [Key]
    public int OrderDetailID {get; set;}

    [ForeignKey("Ingredient")]
    public int IngredientFK {get; set;}
    public Ingredient Ingredient {get; set;}

    [ForeignKey("Order")]
    public int OrderFK {get; set;}
    public Order Order {get; set;}
}