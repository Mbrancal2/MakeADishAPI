using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MakeADishApi.Core.Models;
using MakeADishApi.Application.Services;
using MakeADishApi.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class MakeADishApiController : ControllerBase
{
    private readonly IDbService _dbservice;

    public MakeADishApiController(IDbService dbservice)
    {
        _dbservice = dbservice;
    }

    [HttpGet("Customers")]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomersAsync()
    {
        try {
            IEnumerable<Customer> result_list = await _dbservice.GetAllCustomersAsync();
            return Ok(result_list);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving customers", details = ex.Message});
        }
    }

    [HttpGet("Customers/{customerid:int}")]
    public async Task<ActionResult<Customer>> GetCustomerByIdAsync(int customerid)
    {
        try{
            Customer result = await _dbservice.GetCustomerAsync(customerid);
            if(result == null)
            {
                return NotFound(new {message = "Customer not found" });
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error has occured while trying to get the customer", details = ex.Message});

        }
    }

    [HttpDelete("Customers/{customerid:int}")]
    public async Task<ActionResult> DeleteCustomerAsync(int customerid)
    {
        try{
            (bool success, string message) = await _dbservice.DeleteCustomerAsync(customerid);
            
            if(!success)
            {
                return message == "No matching customer exists in the database"
                    ? NotFound(new{message}) 
                    : StatusCode(500, new{message});
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            //message = "An unexpected error occurred while deleting the customer.", 
            //details = ex.Message 
            return StatusCode(500, new {message = "An error has occured while deleteing a customer", details = ex.Message});
        }
    }

    [HttpPut("Customers/{customerid:int}")]
    public async Task<ActionResult> UpdateCustomerAsync(int customerid, [FromBody] Customer customer)
    {
        if( customerid == null || customer == null)
        {
            return BadRequest("Invalid Customer Data");
        }
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try{
            (bool success, string message) = await _dbservice.UpdateCustomerAsync(customer);

            if(!success)
            {
                return message == "Customer not found"
                ? NotFound(new{message})
                : StatusCode(500, new{message});
            }
            return Ok(message);
        }
        catch(Exception ex)
        {
            return StatusCode(500, new {message = "An unexpected error occurred while updating the customer.", details = ex.Message});
        }
    }

    [HttpPost("Customers/Add")]
    public async Task<IActionResult> AddCustomerAsync([FromBody] Customer customer)
    {
        if(customer == null)
        {
            return BadRequest("Invalid Customer data");
        }
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try{
            (bool success, string message, Customer c) = await _dbservice.AddCustomerAsync(customer);

            if(!success)
            {
             //might need to change customer to c or vice versa.
                return message == "Custumer already exists"
                    ? Conflict(new {message, customer = c})
                    : StatusCode(500, new{message, customer = c});
            }
            return Ok( new {message, StatusCode = 200, customer = c});
        }
        catch(Exception ex)
        {
            return StatusCode(500, new {message = "An unexpected error occurred while adding the customer.", details = ex.Message, customer = customer});
        }
    }

}