using TransactionServices_BankAPI.Data;
using TransactionServices_BankAPI.Dtos;
using TransactionServices_BankAPI.Models;

namespace TransactionServices_BankAPI.Repository;


public class TransactionRepository : ITransactionRepository
{
    private readonly HttpClient _httpClient;
    private AppDbContext _context;
    ILogger<TransactionRepository> _logger;

    public TransactionRepository(AppDbContext context
                                , ILogger<TransactionRepository> logger
                                , HttpClient httpClient)
    {
        _httpClient = httpClient;
        _context = context;
        _logger = logger;
    }





    public async Task<Transaction> MakeDepositAsync(DepositRequest deposit)
    {
        // Create a new deposit transaction record
        var transaction = new Transaction
        {
            TransactionUniqueId = $"{Guid.NewGuid().ToString().Replace("-","").Substring(1, 27)}",
            DestinationAccount = deposit.AccountNumber,
            FullName = deposit.FullName,
            TransactionType = TranType.Deposit,
            TransactionAmount = deposit.Amount,
            TransactionDate = DateTime.UtcNow
        };

        // Implement the logic to update the account balance via HTTP communication with the User Service.
        var balanceUpdateRequest = new DepositRequest
        {
            AccountNumber = deposit.AccountNumber,
            Amount = deposit.Amount
        };

        var balanceUpdateResponse = await _httpClient.PostAsJsonAsync("api/accounts/update-balance", balanceUpdateRequest);
        if (!balanceUpdateResponse.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("Failed to update account balance.");
        }

        // Save the transaction in the local 'Transactions' table.
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return transaction;
    }

    public Response FindTransactionByDate(DateTime? date)
    {
        //Create a new response instance that will return the a transaction details that was successful
        Response response = new Response();
        var transaction = _context.Transactions.Where(x => x.TransactionDate == date).ToList();

        response.ResponCode = "01";
        response.ResponseMessage = "Transaction Details found";
        response.Data = transaction;

        return response;
    }

    public async Task<Transaction> MakeTransferAsync(TransferRequest transfer)
    {

        //Create a new deposit transaction record
        var transaction = new Transaction
        {
            TransactionUniqueId = $"{Guid.NewGuid().ToString().Replace("-","").Substring(1, 27)}",
            SourceAccount = transfer.AccountNumber,
            TransactionType = TranType.Transfer,
            TransactionAmount = transfer.Amount,
            TransactionDate = DateTime.UtcNow,
            DestinationAccount = transfer.DestinationAccountNumber
        };

        // Send a request to the Account services to update the account balance
        var depositRequest = new TransferRequest
        {
            AccountNumber = transfer.DestinationAccountNumber,
            Amount = transfer.Amount
        };

        HttpResponseMessage depositResponse = await _httpClient.PostAsJsonAsync("api/accounts/update-balance", depositRequest);

        if (!depositResponse.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("Deposit Failed");
        }


        // Now let's save the transaction to the local database
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();


        return transaction;
    }
    

    public async Task<Transaction> MakeWithdrawal(WithdrawalRequest withdrawal)
    {
        
        //Create a new deposit transaction record
        var transaction = new Transaction
        {
            TransactionUniqueId = $"{Guid.NewGuid().ToString().Replace("-","").Substring(1, 27)}",
            SourceAccount = withdrawal.AccountNumber,
            TransactionType = TranType.Withdrawal,
            TransactionAmount = withdrawal.Amount,
            TransactionDate = DateTime.UtcNow
        };

        

        // Save the transaction in the local 'Transactions' table
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return transaction;
    }

    /*public async Task<bool> ValidateAccountAsync(string accountNumber)
    {
        var response = await _httpClient.GetAsync($"api/accounts/getByAccountNumber/{accountNumber}");
        return response.IsSuccessStatusCode;
    }*/
}