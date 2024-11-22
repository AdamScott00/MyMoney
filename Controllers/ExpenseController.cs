using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyMoney.Models;
using MyMoney.Services;

namespace MyMoney.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly ExpensesService _expensesService;

    public ExpenseController(ExpensesService expensesService)
    {
        _expensesService = expensesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetExpenses()
    {
        var expenses = await _expensesService.GetExpensesAsync();
        return Ok(expenses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpense(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var result = await _expensesService.GetExpenseAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddExpense([FromBody] ExpenseCreateDto expenseDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var expense = ExpenseFromDto(expenseDto);
        await _expensesService.AddExpenseAsync(expense);
        return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(int id, [FromBody] ExpenseUpdateDto expenseDto)
    {
        var expense = ExpenseFromDto(expenseDto, id);
        if (id != expense.Id)
        {
            return BadRequest();
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await _expensesService.UpdateExpenseAsync(expense);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        await _expensesService.DeleteExpenseAsync(id);
        return NoContent();
    }
    
    
    //Util
    private Expense ExpenseFromDto(ExpenseCreateDto expenseDto)
    {
        return new Expense
        {
            Name = expenseDto.Name,
            Amount = expenseDto.Amount,
            Date = expenseDto.Date
        };
    }
    
    private Expense ExpenseFromDto(ExpenseUpdateDto expenseDto, int id)
    {
        return new Expense
        {
            Id = id,
            Name = expenseDto.Name,
            Amount = expenseDto.Amount,
            Date = expenseDto.Date
        };
    }
}