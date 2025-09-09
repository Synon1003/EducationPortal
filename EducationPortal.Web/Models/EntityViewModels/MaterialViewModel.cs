using System.ComponentModel.DataAnnotations;
using EducationPortal.Web.LanguageResources;

namespace EducationPortal.Web.Models;

public class MaterialViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsDoneByUser { get; set; } = false;
}

public class MaterialCreateViewModel
{
    [Required]
    [StringLength(100)]
    [Display(ResourceType = typeof(Resource), Name = "Title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Type { get; set; } = string.Empty;
}