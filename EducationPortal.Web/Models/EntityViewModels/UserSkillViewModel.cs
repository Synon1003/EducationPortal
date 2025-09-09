using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EducationPortal.Web.LanguageResources;

namespace EducationPortal.Web.Models;

public class UserSkillViewModel
{
    [Display(ResourceType = typeof(Resource), Name = "Skill")]
    public string Name { get; set; } = string.Empty;

    [Display(ResourceType = typeof(Resource), Name = "Level")]
    public int Level { get; set; } = 1;
}
