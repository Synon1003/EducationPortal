using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EducationPortal.Web.LanguageResources;

namespace EducationPortal.Web.Models;

public class ArticleViewModel
{
    public int Id { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "Title")]
    public string Title { get; set; } = "";

    [Display(ResourceType = typeof(Resource), Name = "PublicationDate")]
    public DateOnly PublicationDate { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "ResourceLink")]
    public string ResourceLink { get; set; } = "";
}

public class ArticleCreateViewModel
{
    [Required]
    [StringLength(100)]
    [Display(ResourceType = typeof(Resource), Name = "Title")]
    public string Title { get; set; } = "";

    [Required]
    [DataType(DataType.Date)]
    [Display(ResourceType = typeof(Resource), Name = "PublicationDate")]
    public DateOnly PublicationDate { get; set; }

    [Required]
    [StringLength(500)]
    [Display(ResourceType = typeof(Resource), Name = "ResourceLink")]
    public string ResourceLink { get; set; } = "";
}
