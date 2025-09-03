using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Web.Models;

public class SkillViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}

public class SkillDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    [DisplayName("Acquired by")]
    public int AcquiredCount { get; set; }

    [DisplayName("Highest acquired level")]
    public int AcquiredMaxLevel { get; set; }
}

public class SkillCreateViewModel
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = "";
}