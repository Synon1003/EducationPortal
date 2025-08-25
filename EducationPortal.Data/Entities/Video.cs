using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Data.Entities;

public class Video : Material
{
    [Required]
    [Range(0, 86400)]
    public int Duration { get; set; }

    [Required]
    [StringLength(20)]
    public string Quality { get; set; }
}
