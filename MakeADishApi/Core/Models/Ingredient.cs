using System.ComponentModel.DataAnnotations;

namespace MakeADishApi.Core.Models;


public class Ingredient
{

    public enum IngredientCategory 
    {
        Protein,
        Rice,
        Bean,
        Topping,
        Tortilla,
        Drink,
        Chip_And_Dip,
        Single_Sides
    }
    [Key]
    public int IngredientID {get; set;}
    public string Name {get; set;}
    public IngredientCategory Catagory {get; set;}
    public int Calories {get; set;}
    public decimal? Price {get; set;}
}