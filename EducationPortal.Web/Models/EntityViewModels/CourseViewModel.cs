using System.ComponentModel.DataAnnotations;
using EducationPortal.Web.LanguageResources;

namespace EducationPortal.Web.Models;

public class CourseListViewModel
{
    public int Id { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "Name")]
    public string Name { get; set; } = string.Empty;

    [Display(ResourceType = typeof(Resource), Name = "Description")]
    public string Description { get; set; } = string.Empty;

    [Display(ResourceType = typeof(Resource), Name = "Skills")]
    public List<string> Skills { get; set; } = [];

    [Display(ResourceType = typeof(Resource), Name = "CreatedBy")]
    public string CreatedBy { get; set; } = string.Empty;
    public UserCourseViewModel? UserCourse { get; set; }
}

public class CourseDetailViewModel
{
    public int Id { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "Name")]
    public string Name { get; set; } = string.Empty;

    [Display(ResourceType = typeof(Resource), Name = "Description")]
    public string Description { get; set; } = string.Empty;

    [Display(ResourceType = typeof(Resource), Name = "Skills")]
    public List<string> Skills { get; set; } = [];

    [Display(ResourceType = typeof(Resource), Name = "Materials")]
    public List<string> Materials { get; set; } = [];

    [Display(ResourceType = typeof(Resource), Name = "CreatedBy")]
    public string CreatedBy { get; set; } = string.Empty;
    public UserCourseViewModel? UserCourse { get; set; }
}

public class CourseCreateViewModel
{
    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "NameIsRequiredError")]
    [StringLength(50)]
    [Display(ResourceType = typeof(Resource), Name = "Name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "DescriptionIsRequiredError")]
    [StringLength(250)]
    [Display(ResourceType = typeof(Resource), Name = "Description")]
    public string Description { get; set; } = string.Empty;

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
