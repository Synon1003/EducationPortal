using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationPortal.Data.Entities;

public class UserCourse
{
    [Key]
    public int Id { get; set; }

    [NotMapped]
    public bool IsCompleted => ProgressPercentage == 100;
    public int ProgressPercentage { get; set; } = 0;

    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser? User { get; set; }

    public int CourseId { get; set; }
    [ForeignKey(nameof(CourseId))]
    public virtual Course? Course { get; set; }
}