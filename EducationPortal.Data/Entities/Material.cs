using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationPortal.Data.Entities;

public class Material
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Title { get; set; }

    [Required]
    [StringLength(20)]
    public string Type { get; set; }

    public virtual ICollection<CourseMaterial> CourseMaterials { get; } = [];
}
