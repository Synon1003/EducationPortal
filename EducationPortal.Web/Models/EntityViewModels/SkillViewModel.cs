using System.ComponentModel.DataAnnotations;
using EducationPortal.Web.LanguageResources;

namespace EducationPortal.Web.Models;

public class SkillViewModel
{
    public int Id { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "Name")]
    public string Name { get; set; } = string.Empty;
}

public class SkillDetailViewModel
{
    public int Id { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "Name")]
    public string Name { get; set; } = string.Empty;

    [Display(ResourceType = typeof(Resource), Name = "AcquiredCount")]
    public int AcquiredCount { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "HighestAcquiredLevel")]
    public int AcquiredMaxLevel { get; set; }
}

public class SkillCreateViewModel
{
    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "NameIsRequiredError")]
    [StringLength(50)]
    [Display(ResourceType = typeof(Resource), Name = "Name")]
    public string Name { get; set; } = string.Empty;
}