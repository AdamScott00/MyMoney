using Microsoft.EntityFrameworkCore;
using MyMoney.Models;

namespace MyMoney.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Expense> Expenses { get; set; }
}