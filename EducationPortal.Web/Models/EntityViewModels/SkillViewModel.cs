using System.ComponentModel.DataAnnotations;
using EducationPortal.Web.LanguageResources;

namespace EducationPortal.Web.Models;

public class SkillViewModel
{
    public int Id { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "Name")]
    public string Name { get; set; } = "";
}

public class SkillDetailViewModel
{
    public int Id { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "Name")]
    public string Name { get; set; } = "";

    [Display(ResourceType = typeof(Resource), Name = "AcquiredCount")]
    public int AcquiredCount { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "HighestAcquiredLevel")]
    public int AcquiredMaxLevel { get; set; }
}

public class SkillCreateViewModel
{
    [Required]
    [StringLength(50)]
    [Display(ResourceType = typeof(Resource), Name = "Name")]
    public string Name { get; set; } = "";
}