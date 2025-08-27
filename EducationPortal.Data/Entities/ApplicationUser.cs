using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EducationPortal.Data.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    [StringLength(20)]
    public string Theme { get; set; } = "corporate";

    public virtual ICollection<Course> Courses { get; set; } = [];
}
