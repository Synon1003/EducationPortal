using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EducationPortal.Data.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public string Theme { get; set; }
}
