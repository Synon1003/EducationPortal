using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EducationPortal.Data.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = "";

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = "";

    [Required]
    [StringLength(20)]
    public string Theme { get; set; } = "corporate";

    [Required]
    [StringLength(20)]
    public string Language { get; set; } = "en";

    public virtual ICollection<Course> CreatedCourses { get; set; } = [];
    public virtual ICollection<Course> Courses { get; set; } = [];
    public virtual ICollection<UserCourse> UserCourses { get; set; } = [];

    public virtual ICollection<UserSkill> UserSkills { get; set; } = [];
    public virtual ICollection<Skill> Skills { get; set; } = [];
    public virtual ICollection<Material> Materials { get; set; } = [];
}
