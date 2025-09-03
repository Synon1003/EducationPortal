using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Data.Entities;

public class Skill
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = "";

    public virtual ICollection<Course> Courses { get; set; } = [];
    public virtual ICollection<UserSkill> UserSkills { get; set; } = [];
    public virtual ICollection<ApplicationUser> AcquiredByUsers { get; set; } = [];
}
