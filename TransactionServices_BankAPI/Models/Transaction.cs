using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TransactionServices_BankAPI;


[Table("Transactions")]
public class Transaction
{
    [Key]
    public int Id { get; set; }
    public string TransactionUniqueId { get; set; } //Unique Id for evry transaction
    public decimal TransactionAmount { get; set; }
    public string FullName { get; set; }
    public TranStatus TransactionStatus { get; set; }
    public bool IsSuccessful => TransactionStatus.Equals(TranStatus.Success);

    [Required]
    [RegularExpression (@"^[0][1-9]\d{9}$|^[1-9]\d{9}$", ErrorMessage = "Pin must not be more than 4 digits")]
    public string SourceAccount { get; set; }

    [Required]
    [RegularExpression (@"^[0][1-9]\d{9}$|^[1-9]\d{9}$", ErrorMessage = "Pin must not be more than 4 digits")]
    public string DestinationAccount { get; set; }
    public TranType TransactionType { get; set; }
    public DateTime TransactionDate { get; set; }
    [JsonIgnore]
    public byte[] PinHash { get; set; }
    [JsonIgnore]
    public byte[] PinSalt { get; set; }


    /*public Transaction()
    {
        TransactionUniqueId = $"{Guid.NewGuid().ToString().Replace("-","").Substring(1, 27)}";
    }*/
}

public enum TranStatus
{
    Failed,
    Success,
    Error,
    Pending
}

public enum TranType
{
    Deposit,
    Withdrawal,
    Transfer
}