using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserServices_BankAPI.Models;
using UserServices_BankAPI.Models.Users;

namespace UserServices_BankAPI;


public class AppDbContext : IdentityDbContext<ApplicationUser, AppRole, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<ApplicationUser> Users { get; set;}
}