using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EducationPortal.Data.Entities;

public class Material
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    [StringLength(20)]
    public string Type { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = [];
    public virtual ICollection<ApplicationUser> AcquiredByUsers { get; set; } = [];
}
