using System.ComponentModel.DataAnnotations;
using EducationPortal.Web.LanguageResources;

namespace EducationPortal.Web.Models;

public class PublicationViewModel
{
    public int Id { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "Title")]
    public string Title { get; set; } = "";

    [Display(ResourceType = typeof(Resource), Name = "Authors")]
    public string Authors { get; set; } = "";

    [Display(ResourceType = typeof(Resource), Name = "Pages")]
    public int Pages { get; set; }

    [Display(ResourceType = typeof(Resource), Name = "Format")]
    public string Format { get; set; } = "";

    [Display(ResourceType = typeof(Resource), Name = "PublicationYear")]
    public int PublicationYear { get; set; }
}

public class PublicationCreateViewModel
{
    [Required]
    [StringLength(100)]
    [Display(ResourceType = typeof(Resource), Name = "Title")]
    public string Title { get; set; } = "";

    [Required]
    [StringLength(250)]
    [Display(ResourceType = typeof(Resource), Name = "Authors")]
    public string Authors { get; set; } = "";

    [Required]
    [Range(0, 9999)]
    [Display(ResourceType = typeof(Resource), Name = "Pages")]
    public int Pages { get; set; }

    [Required]
    [StringLength(20)]
    [Display(ResourceType = typeof(Resource), Name = "Format")]
    public string Format { get; set; } = "";

    [Required]
    [Range(-1000, 3000)]
    [Display(ResourceType = typeof(Resource), Name = "PublicationYear")]
    public int PublicationYear { get; set; }
}
