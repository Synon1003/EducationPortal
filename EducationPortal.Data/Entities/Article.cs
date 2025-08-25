using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Data.Entities;

public class Article : Material
{
    [Required]
    [DataType(DataType.Date)]
    public DateOnly PublicationDate { get; set; }

    [Required]
    [StringLength(500)]
    public string ResourceLink { get; set; }
}
