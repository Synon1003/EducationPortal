using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Web.Models;

public class RegisterViewModel
{
    [DisplayName("First Name")]
    [Required(ErrorMessage = "First Name is required.")]
    [StringLength(50)]
    public string FirstName { get; set; } = "";

    [DisplayName("Last Name")]
    [Required(ErrorMessage = "Last Name is required.")]
    [StringLength(50)]
    public string LastName { get; set; } = "";

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email should be in email address format.")]
    [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Email is already taken.")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [DisplayName("Confirm Password")]
    [Required(ErrorMessage = "Confirm Password is required.")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Password and Confirm Password do not match.")]
    public string ConfirmPassword { get; set; } = "";
}
