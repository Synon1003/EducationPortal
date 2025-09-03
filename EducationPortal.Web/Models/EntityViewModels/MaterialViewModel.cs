using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Web.Models;

public class MaterialViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Type { get; set; } = "";
    public bool IsDoneByUser { get; set; } = false;
}

public class MaterialCreateViewModel
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = "";

    [Required]
    [StringLength(20)]
    public string Type { get; set; } = "";
}