using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Web.Models;

public class VideoViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int Duration { get; set; }
    public string Quality { get; set; } = "";
}

public class VideoCreateViewModel
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = "";

    [Required]
    [Range(0, 86400)]
    public int Duration { get; set; }

    [Required]
    [StringLength(20)]
    public string Quality { get; set; } = "";
}
