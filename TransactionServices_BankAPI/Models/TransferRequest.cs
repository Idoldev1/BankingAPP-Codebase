using System.ComponentModel.DataAnnotations;

namespace TransactionServices_BankAPI.Models;


public class TransferRequest
{
    [Required]
    [RegularExpression (@"^[0][1-9]\d{9}$|^[1-9]\d{9}$", ErrorMessage = "Pin must not be more than 4 digits")]
    public string AccountNumber { get; set; }
    public string TransactionPin { get; set; }
    public decimal Amount { get; set; }
    public string DestiantionBankCode { get; set; }
    public string DestinationAccountNumber { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.Now;
    public string Pin { get; set; }
    public TransType TransactionType { get; set; }
}

public enum TransType
{
    Deposit,
    Withdrawal,
    Transfer
}