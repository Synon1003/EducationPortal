using System.ComponentModel.DataAnnotations;
using EducationPortal.Web.LanguageResources;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Web.Models;

public class RegisterViewModel
{
    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FirstNameIsRequiredError")]
    [StringLength(50)]
    [Display(ResourceType = typeof(Resource), Name = "FirstName")]
    public string FirstName { get; set; } = "";

    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "LastNameIsRequiredError")]
    [StringLength(50)]
    [Display(ResourceType = typeof(Resource), Name = "LastName")]
    public string LastName { get; set; } = "";

    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailIsRequiredError")]
    [EmailAddress(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailFormatError")]
    [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailIsAlreadyTakenError")]
    [Display(ResourceType = typeof(Resource), Name = "Email")]
    public string Email { get; set; } = "";

    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PasswordIsRequiredError")]
    [DataType(DataType.Password)]
    [Display(ResourceType = typeof(Resource), Name = "Password")]
    public string Password { get; set; } = "";

    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ConfirmPasswordIsRequiredError")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PasswordAndConfirmPasswordDoNotMatchError")]
    [Display(ResourceType = typeof(Resource), Name = "ConfirmPassword")]
    public string ConfirmPassword { get; set; } = "";
}
