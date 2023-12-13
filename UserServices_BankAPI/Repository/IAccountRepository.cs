using UserServices_BankAPI.Dtos;
using UserServices_BankAPI.Models;
using UserServices_BankAPI.Models.Users;

namespace UserServices_BankAPI.Repository;

public interface IAccountRepository
{
    Account Authenticate(string AccountNumber, string Pin);
    //IEnumerable<Account> GetAllAcount();
    Task<Account> Create(Account account, string Pin, string ConfirmPin);
    void Update(Account account, string? Pin =null);
    void Delete(int Id);
    void UpdateAccountBalance(BalanceUpdate balanceUpdate);
    Account GetById(int Id);
    Account GetByAccountNumber(string AccountNumber);
    Task<ApplicationUser> FindByEmailAsync(string email);
    Task CreateUserAsync(ApplicationUser user);
}