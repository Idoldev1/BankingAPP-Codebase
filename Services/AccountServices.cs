using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UserServices_BankAPI.Dtos;
using UserServices_BankAPI.Models;
using UserServices_BankAPI.Models.Users;
using UserServices_BankAPI.OptionsSetup;
using UserServices_BankAPI.Repository;

namespace UserServices_BankAPI.Services;


public class AccountServices
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private readonly JwtOptions _options;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountServices(IAccountRepository accountRepository
                           ,IMapper mapper
                           ,JwtOptions options
                           ,UserManager<ApplicationUser> userManager)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _options = options;
        _userManager = userManager;
    }



    
    
    public async Task<Account> CreateAccount([FromBody]RegisterNewAccountModel newAccount)
    {
        var account = _mapper.Map<Account>(newAccount);
        var createAccount = await _accountRepository.Create(account, newAccount.Pin, newAccount.ConfirmPin);
        

        return createAccount;
    }

    /*public IEnumerable<GetAccountModel> GetAllAccount()
    {
        var accounts = _accountRepository.GetAllAcount();
        var cleanedAccounts = _mapper.Map<List<GetAccountModel>>(accounts);

        return cleanedAccounts;
    }*/


    public Account Authenticate(Authenticate model)
    {
        var authenticate = _accountRepository.Authenticate(model.AccountNumber, model.Pin);

        return authenticate;
    }



    public GetAccountModel GetByAccountNumber(string AccountNumber)
    {
        var account = _accountRepository.GetByAccountNumber(AccountNumber);
        var cleanedAccounts = _mapper.Map<GetAccountModel>(account);

        return cleanedAccounts;
    }

    public GetAccountModel GetById(int Id)
    {
        var account = _accountRepository.GetById(Id);
        var cleanedAccount = _mapper.Map<GetAccountModel>(account);

        return cleanedAccount;
    }

    public void UpdateAccount(UpdateAccountModel model)
    {
        var account = _mapper.Map<Account>(model);
        _accountRepository.Update(account, model.Pin);
    }
    

    public async Task<string> LoginAsync(Login model)
    {
        var user = await _accountRepository.FindByEmailAsync(model.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            throw new AuthenticationException("Invalid Email or password.");
        }


        // Generate JWT token
        var token = GenerateJwtToken(user);

        return token;

    }



    private string GenerateJwtToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Email),
            new(ClaimTypes.Name, user.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);

    }



    public void UpdateBalance([FromBody] BalanceUpdate balance)
    {
        try
        {
            // Validate the balance.
            if (balance == null || string.IsNullOrWhiteSpace(balance.AccountNumber) || balance.Amount <= 0)
            {
                throw new ArgumentException("Invalid request to update balance.");
            }

            _accountRepository.UpdateAccountBalance(balance);
        }

        catch (Exception ex)
        {
            // Handle exceptions and return an error response.
            throw new Exception("Failed to update account balance: " + ex.Message);
        }
    }
}