
using System.ComponentModel.DataAnnotations;
// using MakeADishApi.Domain.Models;

namespace MakeADishApi.Core.Models;

public class Customer {
    [Key]
    public int CustomerID {get; set;}
    
    public required string FirstName {get; set;}
    public required string LastName {get; set;}
    public ICollection<Order>? Orders {get; set;}    

};