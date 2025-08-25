using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Data.Entities;

public class CourseMaterial
{
    [Key]
    public int Id { get; set; }

    public int CourseId { get; set; }

    public int MaterialId { get; set; }
}