using System.ComponentModel.DataAnnotations;
using EducationPortal.Web.LanguageResources;

namespace EducationPortal.Web.Models;

public class LoginViewModel
{
    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailIsRequiredError")]
    [EmailAddress(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailFormatError")]
    [Display(ResourceType = typeof(Resource), Name = "Email")]
    public string Email { get; set; } = "";

    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PasswordIsRequiredError")]
    [DataType(DataType.Password)]
    [Display(ResourceType = typeof(Resource), Name = "Password")]
    public string Password { get; set; } = "";
}
