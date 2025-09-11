using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EducationPortal.Web.LanguageResources;

namespace EducationPortal.Web.Models;

public class ArticleViewModel
{
    public int Id { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "Title")]
    public string Title { get; set; } = string.Empty;

    [Display(ResourceType = typeof(Resource), Name = "PublicationDate")]
    public DateOnly PublicationDate { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "ResourceLink")]
    public string ResourceLink { get; set; } = string.Empty;
}

public class ArticleCreateViewModel
{
    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TitleIsRequiredError")]
    [StringLength(100)]
    [Display(ResourceType = typeof(Resource), Name = "Title")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PublicationDateIsRequiredError")]
    [DataType(DataType.Date)]
    [Display(ResourceType = typeof(Resource), Name = "PublicationDate")]
    public DateOnly PublicationDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ResourceLinkIsRequiredError")]
    [StringLength(500)]
    [Display(ResourceType = typeof(Resource), Name = "ResourceLink")]
    public string ResourceLink { get; set; } = string.Empty;
}
