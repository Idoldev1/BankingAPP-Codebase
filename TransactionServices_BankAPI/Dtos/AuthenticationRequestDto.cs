using static TransactionServices_BankAPI.Enum.StaticDetails;

namespace TransactionServices_BankAPI;

public class AuthenticationRequestDto
{
    public string AccountNumber { get; set; }
    public string PIN { get; set; }
}