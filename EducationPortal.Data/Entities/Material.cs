using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EducationPortal.Data.Entities;

public class Material
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    [StringLength(20)]
    public string Type { get; set; }

    [JsonIgnore]
    public virtual ICollection<CourseMaterial> CourseMaterials { get; } = [];

    public virtual Video? Video { get; set; }
    public virtual Publication? Publication { get; set; }
    public virtual Article? Article { get; set; }
}
