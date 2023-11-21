using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UserServices_BankAPI.Models.Users;


public class ApplicationUser : IdentityUser
{
    //[Required]
    //public string UserName { get; set; }

    [Required]
    public string Gender { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string Country { get; set; }

    [Required]
    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; }
}