using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace TransactionServices_BankAPI.Data;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Transaction> Transactions { get; set; }
}