using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EducationPortal.Web.LanguageResources;

namespace EducationPortal.Web.Models;

public class CourseListViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public List<string> Skills { get; set; } = [];

    [DisplayName("Created By")]
    public string CreatedBy { get; set; } = "";
    public UserCourseViewModel? UserCourse { get; set; }
}

public class CourseDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public List<string> Skills { get; set; } = [];
    public List<string> Materials { get; set; } = [];

    [DisplayName("Created By")]
    public string CreatedBy { get; set; } = "";
    public UserCourseViewModel? UserCourse { get; set; }
}

public class CourseCreateViewModel
{
    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "Name_IsRequired_Error")]
    [Display(ResourceType = typeof(Resource), Name = "Name_Label")]
    [StringLength(50)]
    public string Name { get; set; } = "";

    [Required]
    [StringLength(250)]
    public string Description { get; set; } = "";

    [DisplayName("Skills")]
    public List<SkillCreateViewModel> Skills { get; set; } = [];
    public List<VideoCreateViewModel> Videos { get; set; } = [];
    public List<PublicationCreateViewModel> Publications { get; set; } = [];
    public List<ArticleCreateViewModel> Articles { get; set; } = [];

    public List<SkillDetailViewModel> ExistingSkills { get; set; } = [];
    public List<VideoViewModel> PreviouslyCreatedVideos { get; set; } = [];
    public List<PublicationViewModel> PreviouslyCreatedPublications { get; set; } = [];
    public List<ArticleViewModel> PreviouslyCreatedArticles { get; set; } = [];

    public List<SkillViewModel> LoadedSkills { get; set; } = [];
    public List<VideoViewModel> LoadedVideos { get; set; } = [];
    public List<PublicationViewModel> LoadedPublications { get; set; } = [];
    public List<ArticleViewModel> LoadedArticles { get; set; } = [];

}
