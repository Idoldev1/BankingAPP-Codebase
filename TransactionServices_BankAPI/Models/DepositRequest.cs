using System.ComponentModel.DataAnnotations;

namespace TransactionServices_BankAPI.Models;

public class DepositRequest
{
    public string FullName { get; set; }

    [Required]
    [RegularExpression (@"^[0][1-9]\d{9}$|^[1-9]\d{9}$", ErrorMessage = "Pin must not be more than 4 digits")]
    public string AccountNumber { get; set; }
    public decimal Amount { get; set; }
    public string Pin { get; set; }
}