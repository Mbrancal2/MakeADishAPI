

using MakeADishApi.Core.Models;

namespace MakeADishApi.Infrastructure.Data;
public static class DbInitilizer
{
    public static async void Initializer( MakeADishContext context)
    {
        context.Database.EnsureCreated();

        if(context.Customers.Any() || context.Ingredients.Any() || context.OrderDetails.Any()
        || context.Orders.Any() || context.OrderDetails.Any())
        {
            return;
        }
        var customers = new Customer[]
        {
            new Customer {CustomerID = 1, FirstName = "John", LastName = "Doe"},
            new Customer {CustomerID = 2, FirstName = "Jane", LastName = "Doe"},
            new Customer {CustomerID = 3, FirstName = "Jack", LastName = "Smith"},
            new Customer {CustomerID = 4, FirstName = "Zack", LastName = "Smith"},
            new Customer {CustomerID = 5, FirstName = "Jill", LastName = "Brown"}
        };

        foreach ( var customer in customers)
        {
            context.Customers.Add(customer);
        }
        context.SaveChanges();

        var Ingredients = new Ingredient[]
        {
            new Ingredient {IngredientID = 1, Name = "Smoked Brisket", Catagory = Ingredient.IngredientCategory.Protein,
            Calories = 360, Price = 12.35M},
            new Ingredient {IngredientID = 2, Name = "Chicken", Catagory = Ingredient.IngredientCategory.Protein,
            Calories = 180, Price = 8.50M},
            new Ingredient {IngredientID = 3, Name = "Steak", Catagory = Ingredient.IngredientCategory.Protein,
            Calories = 150, Price = 10.25M},
            new Ingredient {IngredientID = 4, Name = "Beef Barbacoa", Catagory = Ingredient.IngredientCategory.Protein,
            Calories = 170, Price = 10.25M},
            new Ingredient {IngredientID = 5, Name = "Carnitas", Catagory = Ingredient.IngredientCategory.Protein,
            Calories = 210, Price = 9.15M},
            new Ingredient {IngredientID = 6, Name = "Sofritas", Catagory = Ingredient.IngredientCategory.Protein,
            Calories = 150, Price = 8.50M},
            new Ingredient {IngredientID = 7, Name = "Veggie", Catagory = Ingredient.IngredientCategory.Protein,
            Calories = 0, Price = 8.50M},
            new Ingredient {IngredientID = 8, Name = "Cheese Only", Catagory = Ingredient.IngredientCategory.Protein,
            Calories = 0, Price = 9.05M},
            new Ingredient {IngredientID = 9, Name = "White Rice", Catagory = Ingredient.IngredientCategory.Rice,
            Calories = 210},
            new Ingredient {IngredientID = 10, Name = "Brown Rice", Catagory = Ingredient.IngredientCategory.Rice,
            Calories = 210},
            new Ingredient {IngredientID = 11, Name = "No Rice", Catagory = Ingredient.IngredientCategory.Rice,
            Calories = 0},
            new Ingredient {IngredientID = 12, Name = "Black Beans", Catagory = Ingredient.IngredientCategory.Bean,
            Calories = 130},
            new Ingredient {IngredientID = 13, Name = "Pinto Beans", Catagory = Ingredient.IngredientCategory.Bean,
            Calories = 130},
            new Ingredient {IngredientID = 14, Name = "No Beans", Catagory = Ingredient.IngredientCategory.Bean,
            Calories = 0},
            new Ingredient {IngredientID = 15, Name = "Guacamole", Catagory = Ingredient.IngredientCategory.Topping,
            Calories = 230, Price = 2.60M},
            new Ingredient {IngredientID = 16, Name = "Fresh Tomato Salsa", Catagory = Ingredient.IngredientCategory.Topping,
            Calories = 25},
            new Ingredient {IngredientID = 17, Name = "Roasted Chili-Corn Salsa", Catagory = Ingredient.IngredientCategory.Topping,
            Calories = 80},
            new Ingredient {IngredientID = 18, Name = "Tomatillo-Green Chili Salsa", Catagory = Ingredient.IngredientCategory.Topping,
            Calories = 15},
            new Ingredient {IngredientID = 19, Name = "Tomatillo-Red Chili Salsa", Catagory = Ingredient.IngredientCategory.Topping,
            Calories = 30},
            new Ingredient {IngredientID = 20, Name = "Sour Cream", Catagory = Ingredient.IngredientCategory.Topping,
            Calories = 110},
            new Ingredient {IngredientID = 21, Name = "Fajita Veggies", Catagory = Ingredient.IngredientCategory.Topping,
            Calories = 20},
            new Ingredient {IngredientID = 22, Name = "Cheese", Catagory = Ingredient.IngredientCategory.Topping,
            Calories = 110},
            new Ingredient {IngredientID = 23, Name = "Romaine Lettuce", Catagory = Ingredient.IngredientCategory.Topping,
            Calories = 5},
            new Ingredient {IngredientID = 24, Name = "Queso Blanco", Catagory = Ingredient.IngredientCategory.Topping,
            Calories = 120, Price = 1.40M},
            new Ingredient {IngredientID = 25, Name = "Chipotle-Honey Vinaigrette", Catagory = Ingredient.IngredientCategory.Topping,
            Calories = 220},
            new Ingredient {IngredientID = 26, Name = "No Included Sides", Catagory = Ingredient.IngredientCategory.Topping,
            Calories = 0},
            new Ingredient {IngredientID = 27, Name = "No Included Toppings", Catagory = Ingredient.IngredientCategory.Topping,
            Calories = 0},
            new Ingredient {IngredientID = 28, Name = "Crispy Corn Tortilla", Catagory = Ingredient.IngredientCategory.Tortilla,
            Calories = 200},
            new Ingredient {IngredientID = 29, Name = "Soft Flour Tortilla", Catagory = Ingredient.IngredientCategory.Tortilla,
            Calories = 250}
        };

        foreach(Ingredient ingredient in Ingredients)
        {
            context.Ingredients.Add(ingredient);
        }
        context.SaveChanges();


        var Orders = new Order []
        {
            new Order{OrderID = 1, Dish = Order.OrderDish.Burrito, OrderTime = DateTime.Now, CustomerFK = 1},
            new Order{OrderID = 2, Dish = Order.OrderDish.Burrito_Bowl, OrderTime = DateTime.Now, CustomerFK = 2},
            new Order{OrderID = 3, Dish = Order.OrderDish.Tacos, OrderTime = DateTime.Now, CustomerFK = 3},
            new Order{OrderID = 4, Dish = Order.OrderDish.Quesadilla, OrderTime = DateTime.Now, CustomerFK = 4},
            new Order{OrderID = 5, Dish = Order.OrderDish.Salad, OrderTime = DateTime.Now, CustomerFK = 5}
        };

        foreach ( var order in Orders)
        {
            context.Orders.Add(order);
        }
        context.SaveChanges();

        var OrderDetails = new OrderDetail []
        {
            new OrderDetail{OrderDetailID = 1, IngredientFK = 2, OrderFK = 1}, //order for john doe, burrito, chicken
            new OrderDetail{OrderDetailID = 2, IngredientFK = 6, OrderFK = 1}, //order for john doe, burrito, sofitas
            new OrderDetail{OrderDetailID = 3, IngredientFK = 10, OrderFK = 1}, //order for john doe, burrito, brown rice
            new OrderDetail{OrderDetailID = 4, IngredientFK = 12, OrderFK = 1}, // order for john doe, burrito, black beans
            new OrderDetail{OrderDetailID = 5, IngredientFK = 16, OrderFK = 1}, //order for john doe, burrito, fresh tomato salsa
            new OrderDetail{OrderDetailID = 6, IngredientFK = 19, OrderFK = 1},//order for john doe, burrito, tomatillo-red chili salsa
            new OrderDetail{OrderDetailID = 7, IngredientFK = 20, OrderFK = 1},//order for john doe, burrito, sour cream
            new OrderDetail{OrderDetailID = 8, IngredientFK = 22, OrderFK = 1},//order for john doe, burrito, cheese
            new OrderDetail{OrderDetailID = 9, IngredientFK = 23, OrderFK = 1},//order for john doe, burrito, romaine lettuce
            new OrderDetail{OrderDetailID = 10, IngredientFK = 24, OrderFK = 1},//order for john doe, burrito, queso blanco
            new OrderDetail{OrderDetailID = 11, IngredientFK = 1, OrderFK = 2}, //order for Jane Doe, burrito bowl, Smoked Brisket
            new OrderDetail{OrderDetailID = 12, IngredientFK = 5, OrderFK = 2}, //order for Jane Doe, burrito bowl, Carnitas
            new OrderDetail{OrderDetailID = 13, IngredientFK = 9, OrderFK = 2}, //order for Jane Doe, burrito bowl, White Rice
            new OrderDetail{OrderDetailID = 14, IngredientFK = 10, OrderFK = 2}, //order for Jane Doe, burrito bowl, Brown Rice
            new OrderDetail{OrderDetailID = 15, IngredientFK = 13, OrderFK = 2}, //order for Jane Doe, burrito bowl, Pinto Beans
            new OrderDetail{OrderDetailID = 16, IngredientFK = 15, OrderFK = 2}, //order for Jane Doe, burrito bowl, Guacamole
            new OrderDetail{OrderDetailID = 17, IngredientFK = 21, OrderFK = 2}, //order for Jane Doe, burrito bowl, Fajita Veggies
            new OrderDetail{OrderDetailID = 18, IngredientFK = 23, OrderFK = 2}, //order for Jane Doe, burrito bowl, Romaine Lettuce
            new OrderDetail{OrderDetailID = 19, IngredientFK = 8 , OrderFK = 4}, //order for Zack Smith, Quesadilla, Cheese Only
            new OrderDetail{OrderDetailID = 20, IngredientFK = 21, OrderFK = 4}, //order for Zack Smith, Quesadilla, Fajita Veggies
            new OrderDetail{OrderDetailID = 21, IngredientFK = 16, OrderFK = 4}, //order for Zack Smith, Quesadilla, Fresh Tomato Salsa
            new OrderDetail{OrderDetailID = 22, IngredientFK = 17, OrderFK = 4}, //order for Zack Smith, Quesadilla, Roasted Chili-Corn Salsa
            new OrderDetail{OrderDetailID = 23, IngredientFK = 9, OrderFK = 4}, //order for Zack Smith, Quesadilla, White Rice
            new OrderDetail{OrderDetailID = 24, IngredientFK = 15, OrderFK = 4}, //order for Zack Smith, Quesadilla, Guacamole
            new OrderDetail{OrderDetailID = 25, IngredientFK = 24, OrderFK = 4}, //order for Zack Smith, Quesadilla, Queso Blanco
            new OrderDetail{OrderDetailID = 26, IngredientFK = 3, OrderFK = 5},//order for Jill Brown, Salad, Steak
            new OrderDetail{OrderDetailID = 27, IngredientFK = 4, OrderFK = 5},//order for Jill Brown, Salad, Beef Barbacoa
            new OrderDetail{OrderDetailID = 28, IngredientFK = 9, OrderFK = 5},//order for Jill Brown, Salad, White Rice
            new OrderDetail{OrderDetailID = 29, IngredientFK = 12, OrderFK = 5},//order for Jill Brown, Salad, Black Beans
            new OrderDetail{OrderDetailID = 30, IngredientFK = 13, OrderFK = 5},//order for Jill Brown, Salad, Pinto Beans
            new OrderDetail{OrderDetailID = 31, IngredientFK = 15, OrderFK = 5},//order for Jill Brown, Salad, Guacamole
            new OrderDetail{OrderDetailID = 32, IngredientFK = 16, OrderFK = 5},//order for Jill Brown, Salad, Fresh Tomato Salsa
            new OrderDetail{OrderDetailID = 33, IngredientFK = 17, OrderFK = 5},//order for Jill Brown, Salad, Roasted Chili-Corn Salsa
            new OrderDetail{OrderDetailID = 34, IngredientFK = 18, OrderFK = 5},//order for Jill Brown, Salad, Tomatillo-Green Chili Salsa
            new OrderDetail{OrderDetailID = 35, IngredientFK = 19, OrderFK = 5},//order for Jill Brown, Salad, Tomatillo-Red Chili Salsa
            new OrderDetail{OrderDetailID = 36, IngredientFK = 20, OrderFK = 5},//order for Jill Brown, Salad, Sour Cream
            new OrderDetail{OrderDetailID = 37, IngredientFK = 22, OrderFK = 5},//order for Jill Brown, Salad, Cheese
            new OrderDetail{OrderDetailID = 38, IngredientFK = 24, OrderFK = 5},//order for Jill Brown, Salad Queso Blanco
            new OrderDetail{OrderDetailID = 39, IngredientFK = 25, OrderFK = 5},//order for Jill Brown, Salad, Chipotle-Honey Vinaigrette
            new OrderDetail{OrderDetailID = 40, IngredientFK = 7, OrderFK = 3},//order for Jack Smith, Tacos, Veggie
            new OrderDetail{OrderDetailID = 41, IngredientFK = 28, OrderFK = 3},//order for Jack Smith, Tacos, Crispy Corn Tortilla
            new OrderDetail{OrderDetailID = 42, IngredientFK = 27, OrderFK = 3},//order for Jack Smith, Tacos, No Included Toppings
            new OrderDetail{OrderDetailID = 43, IngredientFK = 15, OrderFK = 3},//order for Jack Smith, Tacos, Guacamole
            new OrderDetail{OrderDetailID = 44, IngredientFK = 24, OrderFK = 3},//order for Jack Smith, Tacos, Queso Blanco
        };

        foreach (var order in OrderDetails)
        {
            context.OrderDetails.Add(order);
        }
        context.SaveChanges();

    }
}