using System.ComponentModel.DataAnnotations;
using EducationPortal.Web.LanguageResources;

namespace EducationPortal.Web.Models;

public class VideoViewModel
{
    public int Id { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "Title")]
    public string Title { get; set; } = "";

    [Display(ResourceType = typeof(Resource), Name = "Duration")]
    public int Duration { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "Quality")]
    public string Quality { get; set; } = "";
}

public class VideoCreateViewModel
{
    [Required]
    [StringLength(100)]
    [Display(ResourceType = typeof(Resource), Name = "Title")]
    public string Title { get; set; } = "";

    [Required]
    [Range(0, 86400)]
    [Display(ResourceType = typeof(Resource), Name = "Duration")]
    public int Duration { get; set; }

    [Required]
    [StringLength(20)]
    [Display(ResourceType = typeof(Resource), Name = "Quality")]
    public string Quality { get; set; } = "";
}
