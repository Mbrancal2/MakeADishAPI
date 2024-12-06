
using MakeADishApi.Core.Models;

namespace MakeADishApi.Core.Interfaces;

public interface IIngredientsRepository
{
    Task<IEnumerable<Ingredient>> GetAllIngredientsAsync();
    Task<IEnumerable<Ingredient>> GetIngredientsByCategoryAsync(Ingredient.IngredientCategory ingredientcategory);
    Task<Ingredient> GetIngredientByIdAsync(int ingredientid);
    Task<Ingredient> GetIngredientByNameAsync(string name);
    Task UpdateIngredientAsync(Ingredient ingredient);
    Task DeleteIngredientAsync(int ingredientid);
    Task AddIngredientAsync(Ingredient ingredient);
}