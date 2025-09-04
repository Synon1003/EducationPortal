using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Data.Entities;

public class Publication : Material
{
    [Required]
    [StringLength(250)]
    public string Authors { get; set; } = "";

    [Required]
    [Range(0, 9999)]
    public int Pages { get; set; }

    [Required]
    [StringLength(20)]
    public string Format { get; set; } = "";

    [Required]
    [Range(-1000, 3000)]
    public int PublicationYear { get; set; }
}
