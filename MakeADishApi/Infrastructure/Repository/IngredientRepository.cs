using MakeADishApi.Core.Interfaces;
using MakeADishApi.Core.Models;
using MakeADishApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MakeADishApi.Infrastructure.Repository;

public class IngredientRepository : IIngredientsRepository
{
    private readonly MakeADishContext _context;

    public IngredientRepository(MakeADishContext context)
    {
        _context = context;
    }

    public async Task AddIngredientAsync(Ingredient ingredient)
    {
        //add result code, check errors, and test/fix

        await _context.Ingredients.AddAsync(ingredient);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteIngredientAsync(int ingredientid)
    {
        //add result code, check errors, and test/fix
        Ingredient ingredient = await _context.Ingredients.FindAsync(ingredientid);

        if(ingredient != null)
        {
            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
        }

    }

    public async Task<IEnumerable<Ingredient>> GetAllIngredientsAsync()
    {
        //add result code, check errors, and test/fix
        return await _context.Ingredients.ToListAsync();
    }

    public async Task<Ingredient> GetIngredientByIdAsync(int ingredientid)
    {
        //add result code, check errors, and test/fix
        return await _context.Ingredients.FindAsync(ingredientid);
    }

    public async Task<Ingredient> GetIngredientByNameAsync(string name)
    {
        //add result code, check errors, and test/fix
        return  await _context.Ingredients.FirstOrDefaultAsync(i => i.Name == name);
    }

    public async Task<IEnumerable<Ingredient>> GetIngredientsByCategoryAsync(Ingredient.IngredientCategory ingredientcategory)
    {
        //add result code, check errors, and test/fix
        return await _context.Ingredients
            .Where( i => i.Catagory == ingredientcategory)
            .ToListAsync();
    }

    public async Task UpdateIngredientAsync(Ingredient ingredient)
    {
        //add result code, check errors, and test/fix
        _context.Ingredients.Update(ingredient);
        await _context.SaveChangesAsync();
    }
}