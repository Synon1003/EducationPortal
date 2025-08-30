using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Web.Models;

public class SkillCreateViewModel
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
}