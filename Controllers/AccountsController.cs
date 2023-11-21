using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using UserServices_BankAPI.Dtos;
using UserServices_BankAPI.Models;
using UserServices_BankAPI.Models.Users;
using UserServices_BankAPI.Services;

namespace UserServices_BankAPI.Controllers;


[ApiController]
[Route("api/accounts")]
public class AccountsController : ControllerBase
{
    private readonly AccountServices _services;

    public AccountsController(AccountServices services)
    {
        _services = services;
    }



    [HttpPost]
    [Route("register-new-account")]
    public IActionResult Create([FromBody] RegisterNewAccountModel createAccount)
    {
        if (!ModelState.IsValid)
            return BadRequest(createAccount);

        var newAccount = _services.CreateAccount(createAccount);
        Log.Information("User creating new account details");

        return Ok(newAccount);
    }


    [HttpPost]
    [Route("login")]
    public IActionResult Login ([FromBody] Login login)
    {
        if (!ModelState.IsValid)
            return BadRequest(login);

        var signIn = _services.LoginAsync(login);

        return Ok(signIn);
    }


    /*[HttpGet]
    [Route("get-all-accounts")]
    public IActionResult GetAllAccount()
    {
        var getAccounts = _services.GetAllAccount;

        return Ok(getAccounts);
    }*/


    [HttpPost]
    [Route("authenticate")]
    public IActionResult Authenticate([FromBody] Authenticate model)
    {
        if (model == null)
            return BadRequest(ModelState);

        _services.Authenticate(model);
        return Ok();
    }



    [Authorize]
    [HttpGet]
    [Route("getByAccountNumber")]
    public IActionResult GetByAccountNumber(string AccountNumber)
    {
        if (!Regex.IsMatch(AccountNumber, @"^[0][1-9]\d{9}$|^[1-9]\d{9}$"))
            return BadRequest("Account Number must be 10 digits only");

        var account = _services.GetByAccountNumber(AccountNumber);

        return Ok(account);
    }


    [HttpGet]
    [Route("get-account-by-Id")]
    public IActionResult GetAccountById(int Id)
    {
        var account = _services.GetById(Id);
        return Ok(account);
    }



    [HttpPut]
    [Route("update")]
    public IActionResult UpdateAccount([FromBody] UpdateAccountModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(model);
            
        _services.UpdateAccount(model);
        return Ok();
    }

    [HttpPost("update-balance")]
    public IActionResult UpdateAccountBalance(BalanceUpdate balanceUpdate)
    {
        if (!ModelState.IsValid)
            return BadRequest(balanceUpdate);

        _services.UpdateBalance(balanceUpdate);
        return Ok();
    }
}