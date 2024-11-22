using MyMoney.Data;
using MyMoney.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Get the connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add DbContext with MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0)),
        mySqlOptions => 
        {
            mySqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5, 
                maxRetryDelay: TimeSpan.FromSeconds(10), 
                errorNumbersToAdd: null 
            );
        })
);

// Register services
builder.Services.AddScoped<ExpensesService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();