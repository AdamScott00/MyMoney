namespace MyMoney.Models;

public class Expense
{
    public int Id { get; set; }
    public String Name { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}

public class ExpenseCreateDto
{
    public String Name { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}

public class ExpenseUpdateDto
{
    public String Name { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}