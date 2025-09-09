using System.ComponentModel.DataAnnotations;
using EducationPortal.Web.LanguageResources;

namespace EducationPortal.Web.Models;

public class PublicationViewModel
{
    public int Id { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "Title")]
    public string Title { get; set; } = string.Empty;

    [Display(ResourceType = typeof(Resource), Name = "Authors")]
    public string Authors { get; set; } = string.Empty;

    [Display(ResourceType = typeof(Resource), Name = "Pages")]
    public int Pages { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "Format")]
    public string Format { get; set; } = string.Empty;

    [Display(ResourceType = typeof(Resource), Name = "PublicationYear")]
    public int PublicationYear { get; set; }
}

public class PublicationCreateViewModel
{
    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TitleIsRequiredError")]
    [StringLength(100)]
    [Display(ResourceType = typeof(Resource), Name = "Title")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "AuthorsIsRequiredError")]
    [StringLength(250)]
    [Display(ResourceType = typeof(Resource), Name = "Authors")]
    public string Authors { get; set; } = string.Empty;

    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PagesIsRequiredError")]
    [Range(0, 9999)]
    [Display(ResourceType = typeof(Resource), Name = "Pages")]
    public int Pages { get; set; }

    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FormatIsRequiredError")]
    [StringLength(20)]
    [Display(ResourceType = typeof(Resource), Name = "Format")]
    public string Format { get; set; } = string.Empty;

    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PublicationYearIsRequiredError")]
    [Range(-1000, 3000)]
    [Display(ResourceType = typeof(Resource), Name = "PublicationYear")]
    public int PublicationYear { get; set; }
}
