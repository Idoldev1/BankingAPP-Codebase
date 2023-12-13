namespace TransactionServices_BankAPI.Models;


public class WithdrawalRequest
{
    public string AccountNumber { get; set; }
    public decimal Amount { get; set; }
    public string Pin { get; set; }
}