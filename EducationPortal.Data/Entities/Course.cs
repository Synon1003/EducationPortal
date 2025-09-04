using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationPortal.Data.Entities;

public class Course
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = "";

    [Required]
    [StringLength(250)]
    public string Description { get; set; } = "";

    public virtual ICollection<Skill> Skills { get; set; } = [];

    public virtual ICollection<Material> Materials { get; set; } = [];

    public Guid CreatedBy { get; init; }

    [ForeignKey(nameof(CreatedBy))]
    public virtual ApplicationUser? CreatedByUser { get; }

    public virtual ICollection<ApplicationUser> Users { get; set; } = [];
    public virtual ICollection<UserCourse> UserCourses { get; set; } = [];
}
