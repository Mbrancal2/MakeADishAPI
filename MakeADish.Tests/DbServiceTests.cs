using Moq;
using Xunit;
using MakeADishApi.Application.Services;
using MakeADishApi.Core.Models;
using MakeADishApi.Core.Interfaces;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Crypto.Prng;
using MySqlX.XDevAPI.Common;

public class DbServiceTests
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly DbService _dbservice; 

    public DbServiceTests()
    {
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _dbservice = new DbService(_customerRepositoryMock.Object, null, null, null);
    }

    //tests for AddCutomerAsync();
    [Fact]
    public async Task AddCustomerAsync_CustomerAlreadyExists_ReturnConflict()
    {
        //Arrange
        var customer = new Customer { CustomerID = 1, FirstName = "John", LastName = "Doe"};
        _customerRepositoryMock.Setup( repo => repo.GetCustomerAsync(It.IsAny<int>())).ReturnsAsync(customer);

        //Act
        var result = await _dbservice.AddCustomerAsync(customer);

        //Assert
        Assert.False(result.success);
        Assert.Equal("Customer alredy exists", result.message);

    }

    [Fact]
    public async Task AddCustomerAsync_FailedToAddCustomer()
    {
        //Arrange
        var customer = new Customer { CustomerID = 6, FirstName = "James", LastName = "Brown"};
        _customerRepositoryMock.Setup( repo => repo.AddCustomerAsync(It.IsAny<Customer>())).ReturnsAsync(false);

        //Act
        var result = await _dbservice.AddCustomerAsync(customer);

        //Assert
        Assert.False(result.success);
        Assert.Equal(result.message, "Failed to add customer to database");
        Assert.Equal(result.c, customer);
    }

    [Fact]
    public async Task AddCustomerAsync_PassedAddCustomer()
    {
        var customer = new Customer { CustomerID = 6, FirstName = "James", LastName = "Brown"};
        _customerRepositoryMock.Setup(repo => repo.GetCustomerAsync(customer.CustomerID)).ReturnsAsync((Customer)null); //customer does not exist.
        _customerRepositoryMock.Setup(repo => repo.AddCustomerAsync(customer)).ReturnsAsync(true); //customer added to the database.

        //Act
        var result = await _dbservice.AddCustomerAsync(customer);

        //Assert
        Assert.True(result.success);
        Assert.Equal(result.message, "Customer added to database");
        Assert.Equal(result.c, customer);
    }

    [Fact]
    public async Task AddCustomerAsync_Failed_UnexpectedError()
    {
        // Arrange
        var customer = new Customer { CustomerID = 7, FirstName = "Tyler", LastName = "Red"};
        string exceptionMessage = "simulated exception";
        _customerRepositoryMock.Setup(repo => repo.GetCustomerAsync(customer.CustomerID)).ReturnsAsync((Customer)null); //customer does not exist.
        _customerRepositoryMock.Setup(repo => repo.AddCustomerAsync(customer)).ThrowsAsync(new Exception(exceptionMessage));//throw exception message.

        //Act
        var result = await _dbservice.AddCustomerAsync(customer);

        //Assert
        Assert.False(result.success);
        Assert.Contains("Failed to add customer to databse due to an unexpected error", result.message);
        Assert.Contains(exceptionMessage, result.message);
        Assert.Equal(customer, result.c);
    }
    //end tests for AddCustomerAsync();
    //start tests for DeleteCustomerAsync();
    [Fact]
    public async Task DeleteCustomerAsync_Failed_NoMatchingCustomer()
    {
        //Arrange
        var customer = new Customer {CustomerID = 1, FirstName = "John", LastName = "Doe"};
        _customerRepositoryMock.Setup(repo => repo.GetCustomerAsync(customer.CustomerID)).ReturnsAsync((Customer)null); //customer already exists in the database.

        //Act
        var result = await _dbservice.DeleteCustomerAsync(customer.CustomerID);

        //Assert
        Assert.False(result.success);
        Assert.Equal("No matching customer exists in the database", result.message);
    }
    [Fact]
    public async Task DeleteCustomerAsync_Passed_CustomerDeletedSuccessfully()
    {
        //Arrange
        var customer = new Customer {CustomerID = 1, FirstName = "John", LastName = "Doe"};
        _customerRepositoryMock.Setup(repo => repo.GetCustomerAsync(customer.CustomerID)).ReturnsAsync(customer); //customer already exists in the database.
        _customerRepositoryMock.Setup(repo => repo.DeleteCustomerAsync(customer.CustomerID)).ReturnsAsync(true);
        //Act
        var result = await _dbservice.DeleteCustomerAsync(customer.CustomerID);

        //Assert
        Assert.True(result.success);
        Assert.Equal("Customer with id: 1 deleted successfully", result.message);
        Assert.Contains("1", result.message);
        _customerRepositoryMock.Verify(repo => repo.GetCustomerAsync(customer.CustomerID), Times.Once); 
        _customerRepositoryMock.Verify(repo => repo.DeleteCustomerAsync(customer.CustomerID), Times.Once);
    }

    [Fact]
    public async Task DeleteCustomerAsync_Failed_FailedToDeleteCustomerWithId()
    {
        //Arrange
        var customer =  new Customer {CustomerID = 1, FirstName = "John", LastName = "Doe"};
        _customerRepositoryMock.Setup(repo => repo.GetCustomerAsync(customer.CustomerID)).ReturnsAsync(customer); //customer already exists in the database.
        _customerRepositoryMock.Setup(repo => repo.DeleteCustomerAsync(customer.CustomerID)).ReturnsAsync(false);

        //Act
        var result = await _dbservice.DeleteCustomerAsync(customer.CustomerID);

        //Assert
        Assert.False(result.success);
        Assert.Equal("Failed to delete customer with id: 1", result.message);
        Assert.Contains("1", result.message);
        _customerRepositoryMock.Verify(repo => repo.GetCustomerAsync(customer.CustomerID), Times.Once); 
        _customerRepositoryMock.Verify(repo => repo.DeleteCustomerAsync(customer.CustomerID), Times.Once);

        
    }
    [Fact]
    public async Task DeleteCustomerAsync_Failed_Exception()
    {
        //Arrange
        var customer =  new Customer {CustomerID = 1, FirstName = "John", LastName = "Doe"};
        string exceptionMessage = "simulated exception";
        _customerRepositoryMock.Setup(repo => repo.GetCustomerAsync(customer.CustomerID)).ReturnsAsync(customer); //customer already exists in the database.
        _customerRepositoryMock.Setup(repo => repo.DeleteCustomerAsync(customer.CustomerID)).ThrowsAsync(new Exception(exceptionMessage));

        //Act
        var result = await _dbservice.DeleteCustomerAsync(customer.CustomerID);

        //Assert
        Assert.False(result.success);
        Assert.Contains("Failed to delete customer due to an unexpected error:", result.message);
        Assert.Contains("simulated exception", result.message);
        _customerRepositoryMock.Verify(repo => repo.GetCustomerAsync(customer.CustomerID), Times.Once); 
        _customerRepositoryMock.Verify(repo => repo.DeleteCustomerAsync(customer.CustomerID), Times.Once);
        
    }
    //end tests for DeleteCustomerAsync();
    //start tests for GetAllCustomersAsync();
    [Fact]
    public async Task GetAllCustomersAsync_Passed()
    {
        //Arrange 
        var expectedCustomers = new List<Customer> {
            new Customer { CustomerID = 1, FirstName = "John", LastName = "Doe" }, 
            new Customer { CustomerID = 2, FirstName = "Jane", LastName = "Smith" } 
        };

        _customerRepositoryMock.Setup(repo => repo.GetAllCustomersAsync()).ReturnsAsync(expectedCustomers);

        //Act
        var result = await _dbservice.GetAllCustomersAsync();

        //Assert
        Assert.NotNull(result); 
        Assert.Equal(expectedCustomers.Count, result.Count()); 
        Assert.Equal(expectedCustomers, result);

        _customerRepositoryMock.Verify(repo => repo.GetAllCustomersAsync(), Times.Once);
    }
    [Fact]
    public async Task GetAllCustomersAsync_Failed_thrownException()
    {
        //Arrange
        var expectedCustomers = new List<Customer> {
            new Customer { CustomerID = 1, FirstName = "John", LastName = "Doe" }, 
            new Customer { CustomerID = 2, FirstName = "Jane", LastName = "Smith" } 
        };
        string exceptionMessage = "simulated exception";

        _customerRepositoryMock.Setup(repo => repo.GetAllCustomersAsync()).ThrowsAsync(new Exception(exceptionMessage));
        
        //Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _dbservice.GetAllCustomersAsync());

        //Assert
        Assert.Equal("An error occurred while getting customers: simulated exception", exception.Message);
        Assert.Contains("An error occurred while getting customers", exception.Message); 
        Assert.Contains(exceptionMessage, exception.Message);

    }
    //end tests for GetAllCustomersAsync();
    //start tests for GetCustomerAsync();
    [Fact]
    public async Task GetCustomerAsync_Passed()
    {
        //Arrange 
        var expectedCustomer = new Customer { CustomerID = 1, FirstName = "John", LastName = "Doe" };

        _customerRepositoryMock.Setup(repo => repo.GetCustomerAsync(expectedCustomer.CustomerID)).ReturnsAsync(expectedCustomer);

        //Act
        var result = await _dbservice.GetCustomerAsync(expectedCustomer.CustomerID);

        //Assert
        Assert.NotNull(result);  
        Assert.Equal(expectedCustomer, result);

        _customerRepositoryMock.Verify(repo => repo.GetCustomerAsync(expectedCustomer.CustomerID), Times.Once);
    }
    [Fact]
    public async Task GetCustomerAsync_Failed_thrownException()
    {
        //Arrange
        var expectedCustomer = new Customer { CustomerID = 1, FirstName = "John", LastName = "Doe" };

        string exceptionMessage = "simulated exception";

        _customerRepositoryMock.Setup(repo => repo.GetCustomerAsync(expectedCustomer.CustomerID)).ThrowsAsync(new Exception(exceptionMessage));
        
        //Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _dbservice.GetCustomerAsync(expectedCustomer.CustomerID));

        //Assert
        Assert.Equal("An error occurred while getting customer: simulated exception", exception.Message);
        Assert.Contains("An error occurred while getting customer", exception.Message); 
        Assert.Contains(exceptionMessage, exception.Message);

        _customerRepositoryMock.Verify(repo => repo.GetCustomerAsync(expectedCustomer.CustomerID), Times.Once);

    }
    //end tests for GetCustomerAsync();
    //start tests for UpdateCustomerAsync();
    [Fact]
    public async Task UpdateCustomerAsync_Failed_CustomerNotFound()
    {
        //Arrange
        var customer = new Customer { CustomerID = 1, FirstName = "John", LastName = "Doe" };
        _customerRepositoryMock.Setup(repo => repo.GetCustomerAsync(customer.CustomerID)).ReturnsAsync((Customer)null);

        //Act
        var result = await _dbservice.UpdateCustomerAsync(customer);

        //Assert
        Assert.False(result.success);
        Assert.Equal("Customer not found", result.message);

        _customerRepositoryMock.Verify(repo => repo.GetCustomerAsync(customer.CustomerID), Times.Once);

    }
    [Fact]
    public async Task UpdateCustomerAsync_Passed_CustomerUpdatedSuccessfully()
    {
        //Arrange
        var customer = new Customer { CustomerID = 1, FirstName = "John", LastName = "Doe" };
        _customerRepositoryMock.Setup(repo => repo.GetCustomerAsync(customer.CustomerID)).ReturnsAsync(customer);
        _customerRepositoryMock.Setup(repo => repo.UpdateCustomerAsync(customer)).ReturnsAsync(true);
        
        //Act
        var result = await _dbservice.UpdateCustomerAsync(customer);

        //Assert
        Assert.True(result.success);
        Assert.Equal(result.message, "Customer updated successfully");

        _customerRepositoryMock.Verify(repo => repo.GetCustomerAsync(customer.CustomerID), Times.Once);
        _customerRepositoryMock.Verify(repo => repo.UpdateCustomerAsync(customer), Times.Once);

    }
    [Fact]
    public async Task UpdateCustomerAsync_Failed_FailedToUpdateCustomer()
    {
        //Arrange
        var customer = new Customer { CustomerID = 1, FirstName = "John", LastName = "Doe" };
        _customerRepositoryMock.Setup(repo => repo.GetCustomerAsync(customer.CustomerID)).ReturnsAsync(customer);
        _customerRepositoryMock.Setup(repo => repo.UpdateCustomerAsync(customer)).ReturnsAsync(false);
        
        //Act
        var result = await _dbservice.UpdateCustomerAsync(customer);

        //Assert
        Assert.False(result.success);
        Assert.Equal(result.message, "Failed to update customer");

        _customerRepositoryMock.Verify(repo => repo.GetCustomerAsync(customer.CustomerID), Times.Once);
        _customerRepositoryMock.Verify(repo => repo.UpdateCustomerAsync(customer), Times.Once);

    }
    [Fact]
    public async Task UpdateCustomerAsync_Failed_FailedToUpdateCustomerUnespectedError()
    {
        //Arrange
        var customer = new Customer { CustomerID = 1, FirstName = "John", LastName = "Doe" };
        var exceptionMessage = "simulated exception";
        _customerRepositoryMock.Setup(repo => repo.GetCustomerAsync(customer.CustomerID)).ReturnsAsync(customer);
        //_customerRepositoryMock.Setup(repo => repo.UpdateCustomerAsync(customer)).ThrowsAsync(new Exception(exceptionMessage));
        _customerRepositoryMock.Setup(repo => repo.UpdateCustomerAsync(customer)).ThrowsAsync(new Exception(exceptionMessage));
        
        //Act
        //var result = await _dbservice.UpdateCustomerAsync(customer);
        var result = await _dbservice.UpdateCustomerAsync(customer); 
        //Assert 
        Assert.False(result.success); 
        Assert.Contains("Failed to update customer due to an unexpected error", result.message); 
        Assert.Contains(exceptionMessage, result.message);

        _customerRepositoryMock.Verify(repo => repo.GetCustomerAsync(customer.CustomerID), Times.Once);
        _customerRepositoryMock.Verify(repo => repo.UpdateCustomerAsync(customer), Times.Once);

    }
    //end tests for UpdateCustomerAsync();



}