using System.Net.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TransactionServices_BankAPI.Data;
using TransactionServices_BankAPI.Repository;
using TransactionServices_BankAPI.Services;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().
    MinimumLevel.Information()
    .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Hour)
    .CreateLogger();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Add HttpClient with named client
builder.Services.AddHttpClient("UserServicesAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7219/");

    // Configure other HttpClient settings if needed

    // Configure SSL certificate handling for self-signed certificates
});


builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddTransient<TransactionService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
