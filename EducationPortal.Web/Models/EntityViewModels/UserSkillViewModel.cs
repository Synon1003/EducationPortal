using System.ComponentModel;

namespace EducationPortal.Web.Models;

public class UserSkillViewModel
{
    [DisplayName("Skill")]
    public string Name { get; set; }
    public int Level { get; set; } = 1;
}
