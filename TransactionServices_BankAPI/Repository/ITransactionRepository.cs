using TransactionServices_BankAPI.Dtos;
using TransactionServices_BankAPI.Models;

namespace TransactionServices_BankAPI.Repository;

public interface ITransactionRepository
{
    //Task<bool> ValidateAccountAsync(string accountNumber);
    Task<Transaction> MakeDepositAsync(DepositRequest deposit);
    Response FindTransactionByDate(DateTime? date);
    Task<Transaction> MakeTransferAsync(TransferRequest transfer);
    Task<Transaction> MakeWithdrawal(WithdrawalRequest withdrawal);
}