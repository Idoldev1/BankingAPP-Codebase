using Microsoft.AspNetCore.Mvc;
using Serilog;
using TransactionServices_BankAPI.Models;
using TransactionServices_BankAPI.Services;

namespace TransactionServices_BankAPI.Controller;


[ApiController]
[Route("api/transactions")]
public class TransactionController : ControllerBase
{
    private readonly TransactionService _service;

    public TransactionController(TransactionService service)
    {
        _service = service;
    }



    [HttpPost]
    [Route("transfer")]
    public async Task<IActionResult> MakeTransfer([FromBody]TransferRequest transferRequest)
    {
        try
        {
            var transaction = await _service.MakeNewTransferAsync(transferRequest);
            Log.Information($"Transfer action successful for user with details {transaction}");
            return Ok(transaction);
        }

        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost]
    [Route("deposit")]
    public async Task<IActionResult> MakeDeposit(DepositRequest deposit)
    {
        var transaction = await _service.MakeNewDepositAsync(deposit);
        Log.Information($"Deposit action successful for user with details {transaction}");
        return Ok(transaction);
    }


    [HttpGet]
    [Route("withdrawal")]
    public async Task<IActionResult> Withdrawal(WithdrawalRequest withdrawal)
    {
        var newWithdrawal = await _service.MakeWithdrawalAsync(withdrawal);
        Log.Information($"Withdrawal successful for user with details {newWithdrawal}");
        return Ok(newWithdrawal);
    }


    [HttpGet]
    [Route("getTransactionByDate")]
    public IActionResult FindTransactionByDate(DateTime dateTime)
    {
        var request = _service.GetTransactionByDate(dateTime);
        Log.Information($"Transaction Details requested for {dateTime} with details {request}");

        return Ok(request);
    }
}