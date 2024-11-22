using Microsoft.AspNetCore.Mvc;
using Moq;
using MyMoney.Controllers;
using MyMoney.Services;
using MyMoney.Models;

namespace MyMoney.Tests;

public class ExpenseControllerTests
{
    [Fact]
    public async Task GetExpens_ReturnsOk_WhenExpenseExists()
    {
        // Arrange
        var mockService = new Mock<IExpensesService>();
        var expenseId = 1;
        var expectedExpense = new Expense
        {
            Id = expenseId,
            Name = "Groceries",
            Amount = 200,
            Date = DateTime.Now,
        };

        mockService
            .Setup(service => service.GetExpenseAsync(expenseId))
            .ReturnsAsync(expectedExpense);
        
        var controller = new ExpenseController(mockService.Object);
        
        // Act
        var result = await controller.GetExpense(expenseId);
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var resultExpense = Assert.IsType<Expense>(okResult.Value);
        Assert.Equal(expectedExpense.Id, resultExpense.Id);
        Assert.Equal(expectedExpense.Name, resultExpense.Name);
        Assert.Equal(expectedExpense.Amount, resultExpense.Amount);
    }
}