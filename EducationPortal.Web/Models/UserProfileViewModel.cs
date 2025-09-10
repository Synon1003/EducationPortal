using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EducationPortal.Web.LanguageResources;

namespace EducationPortal.Web.Models;

public class UserProfileViewModel
{
    [Display(ResourceType = typeof(Resource), Name = "FirstName")]
    public string FirstName { get; set; } = string.Empty;

    [Display(ResourceType = typeof(Resource), Name = "LastName")]
    public string LastName { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    [Display(ResourceType = typeof(Resource), Name = "Email")]
    public string Email { get; set; } = string.Empty;

    public List<VideoViewModel> Videos { get; set; } = [];
    public List<PublicationViewModel> Publications { get; set; } = [];
    public List<ArticleViewModel> Articles { get; set; } = [];
    public List<UserSkillViewModel> UserSkills { get; set; } = [];
}
