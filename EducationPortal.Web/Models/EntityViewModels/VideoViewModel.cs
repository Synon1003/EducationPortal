using System.ComponentModel.DataAnnotations;
using EducationPortal.Web.LanguageResources;

namespace EducationPortal.Web.Models;

public class VideoViewModel
{
    public int Id { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "Title")]
    public string Title { get; set; } = string.Empty;

    [Display(ResourceType = typeof(Resource), Name = "Duration")]
    public int Duration { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "Quality")]
    public string Quality { get; set; } = string.Empty;
}

public class VideoCreateViewModel
{
    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TitleIsRequiredError")]
    [StringLength(100)]
    [Display(ResourceType = typeof(Resource), Name = "Title")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "DurationIsRequiredError")]
    [Range(0, 86400)]
    [Display(ResourceType = typeof(Resource), Name = "Duration")]
    public int Duration { get; set; }

    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "QualityIsRequiredError")]
    [StringLength(20)]
    [Display(ResourceType = typeof(Resource), Name = "Quality")]
    public string Quality { get; set; } = string.Empty;
}
