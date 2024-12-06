
//using MakeADishApi.Domain;
using MakeADishApi.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MakeADishApi.Infrastructure.Data;
public class MakeADishContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public MakeADishContext(DbContextOptions<MakeADishContext> options) : base(options)
    { 
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingredient>()
            .Property(i => i.Catagory)
            .HasConversion<string>();

        modelBuilder.Entity<Order>()
            .Property( o => o.Dish)
            .HasConversion<string>();

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerFK);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Ingredient)
            .WithMany()
            .HasForeignKey(od => od.IngredientFK);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(od => od.OrderDetails)
            .HasForeignKey(od => od.OrderFK);

        // modelBuilder.Entity<Customer>()
        // .HasMany(c => c.Orders)
        // .WithOne(c => c.Customer)
        // .HasForeignKey(c => c.CustomerID)
        // .IsRequired(false);

        // modelBuilder.Entity<Order>()
        //     .HasOne(o => o.Customer)
        //     .WithMany(c => c.Orders)
        //     .HasForeignKey(o => o.CustomerID);
        

        // modelBuilder.Entity<Order>()
        // .HasOne(o => o.Customer)
        
        


        // modelBuilder.Entity<Order>()
        // .Property( o => o.Dish)
        //.HasConversion<string>();
    }
}