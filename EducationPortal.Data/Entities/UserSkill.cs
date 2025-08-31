using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationPortal.Data.Entities;

public class UserSkill
{
    public int Level { get; set; } = 0; // TODO:  = 1 migration

    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; }

    public int SkillId { get; set; }
    [ForeignKey(nameof(SkillId))]
    public virtual Skill Skill { get; set; }
}