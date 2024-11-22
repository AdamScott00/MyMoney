using Microsoft.EntityFrameworkCore;
using MyMoney.Data;
using MyMoney.Models;

namespace MyMoney.Services;

public class ExpensesService
{
    private readonly ApplicationDbContext _context;

    public ExpensesService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddExpenseAsync(Expense expense)
    {
        //Validation
        if (expense.Amount <= 0 || expense.Date > DateTime.Now || expense.Name.Length <= 1)
        {
            throw new ArgumentException("Invalid data. Validation Failed");
        }
        //Call repo
        await _context.Expenses.AddAsync(expense);
        await _context.SaveChangesAsync();
    }

    public async Task<Expense?> GetExpenseAsync(int expenseId)
    {
        return await _context.Expenses.FindAsync(expenseId);
    }

    public async Task<IEnumerable<Expense>> GetExpensesAsync()
    {
        return await _context.Expenses.ToListAsync();
    }

    public async Task UpdateExpenseAsync(Expense updatedExpense)
    {
        var originalExpense = await GetExpenseAsync(updatedExpense.Id);
        if (originalExpense == null)
        {
            throw new ArgumentException("Expense not found");
        }

        if (originalExpense == updatedExpense)
        {
            throw new ArgumentException("Nothing changed");
        }

        updatedExpense.Id = originalExpense.Id;
        
        _context.Expenses.Update(updatedExpense);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteExpenseAsync(int expenseId)
    {
        var expense = await _context.Expenses.FindAsync(expenseId);
        if (expense == null)
        {
            throw new ArgumentNullException(nameof(expense));
        }
        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
    }
}