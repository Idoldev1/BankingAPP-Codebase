using System.ComponentModel.DataAnnotations;

namespace UserServices_BankAPI.Models.Users;


public class Register
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}