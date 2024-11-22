using MyMoney.Models;

namespace MyMoney.Services;

public interface IExpensesService
{
    Task<Expense?> GetExpenseAsync(int id);
    Task<IEnumerable<Expense>> GetExpensesAsync();
    Task AddExpenseAsync(Expense expense);
    Task UpdateExpenseAsync(Expense expense);
    Task DeleteExpenseAsync(int id);
}
