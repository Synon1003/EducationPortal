using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationPortal.Data.Entities;

public class Course
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(250)]
    public string Description { get; set; }

    public virtual ICollection<CourseSkill> CourseSkills { get; set; } = [];
    public virtual ICollection<CourseMaterial> CourseMaterials { get; set; } = [];

    [NotMapped]
    public virtual ICollection<Skill> Skills => [.. CourseSkills.Select(cs => cs.Skill)];

    [NotMapped]
    public virtual ICollection<Material> Materials => [.. CourseMaterials.Select(cm => cm.Material)];
}
