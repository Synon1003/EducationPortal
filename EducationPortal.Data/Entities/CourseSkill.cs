using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Data.Entities;

public class CourseSkill
{
    [Key]
    public int Id { get; set; }

    public int CourseId { get; set; }

    public int SkillId { get; set; }
}