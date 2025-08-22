using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationPortal.Data.Entities;

public class CourseMaterial
{
    [Key]
    public int Id { get; set; }

    public int CourseId { get; set; }

    [ForeignKey(nameof(CourseId))]
    public virtual Course Course { get; set; }

    public int MaterialId { get; set; }

    [ForeignKey(nameof(MaterialId))]
    public virtual Material Material { get; set; }
}