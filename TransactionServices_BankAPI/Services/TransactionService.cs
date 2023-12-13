using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TransactionServices_BankAPI.Dtos;
using TransactionServices_BankAPI.Models;
using TransactionServices_BankAPI.Repository;

namespace TransactionServices_BankAPI.Services;



public class TransactionService 
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;

    public TransactionService(ITransactionRepository transactionRepository, IMapper mapper, HttpClient httpClient)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
        _httpClient = httpClient;
    }


    public async Task<Transaction> MakeNewTransferAsync(TransferRequest request)
    {
        // Use the validate method to check the authenticity of the account number and pin received
        if (!await ValidateAccountAsync(request.AccountNumber, request.Pin))
        {
            throw new InvalidOperationException("Source account/pin is not valid");
        }

        // Add vallidation for minimum and maximum amount accepted
        if (request.Amount <= 10 || request.Amount >= 100001)
        {
            throw new ArgumentException("Invalid deposit data. Please input an amount greater than Ten");
        }

        // If all conditions above are met, make a call to the repository to execute the main login
        var transaction = await _transactionRepository.MakeTransferAsync(request);
        
        return transaction;
    }



    public async Task<Transaction> MakeNewDepositAsync(DepositRequest depositRequest)
    {
        // Use the validate method to check the authenticity of the account number and pin received
        if (!await ValidateAccountAsync(depositRequest.AccountNumber, depositRequest.Pin))
        {
            throw new InvalidOperationException("Source account is not valid");
        }

        // Add validation logic for the deposit amount and other requirements here.
        if ( depositRequest.Amount <= 50 || depositRequest.Amount >= 500001 || depositRequest.FullName == null)
        {
            throw new ArgumentException("Invalid deposit data.");
        }

        // Invoke the repository to handle deposit logic
        var transaction = await _transactionRepository.MakeDepositAsync(depositRequest);

        return transaction;
    }



    public Response GetTransactionByDate(DateTime date)
    {
        // Check if the input date is within an acceptable range, e.g., not in the future or empty
        if (string.IsNullOrEmpty(date.ToString()) || date > DateTime.Now)
        {
            throw new ArgumentException("Invalid date: Transaction date cannot be empty/in the future.");
        }

        // You can call the repository to execute logic
        var request = _transactionRepository.FindTransactionByDate(date);

        return request;

    }



    public async Task<Transaction> MakeWithdrawalAsync(WithdrawalRequest withdrawalRequest)
    {

        // Add validation logic for the withdrawal amount and other requirements here.
        if (!await ValidateAccountAsync(withdrawalRequest.AccountNumber, withdrawalRequest.Pin))
        {
            throw new InvalidOperationException("Source account/pin is not valid");
        }

        // Add vallidation for minimum and maximum amount accepted
        if (withdrawalRequest.Amount <= 999 || withdrawalRequest.Amount >= 20000)
        {
            throw new ArgumentException("Please input an amount between 1000 - 20,000");
        }

        var withdraw = await _transactionRepository.MakeWithdrawal(withdrawalRequest);

        return withdraw;
    }




    public async Task<bool> ValidateAccountAsync(string accountNumber, string pin)
    {
        var authenticationRequest = new AuthenticationRequestDto
        {
            AccountNumber = accountNumber,
            PIN = pin
        };

        if (string.IsNullOrWhiteSpace(authenticationRequest.AccountNumber))
        {
            throw new ArgumentException("Invalid data. Please input a valid account number");
        }

        var response = await _httpClient.PostAsJsonAsync("api/accounts/authenticate", authenticationRequest);
        
        if (response.IsSuccessStatusCode)
        {
            // Authentication was successful
            return true;
        }

        // Authentication failed
        return false;
    }
}