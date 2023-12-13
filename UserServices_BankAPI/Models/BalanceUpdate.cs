namespace UserServices_BankAPI.Models;


public class BalanceUpdate
{
    public string AccountNumber { get; set; }
    public decimal Amount { get; set; }
}