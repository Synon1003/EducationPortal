using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EducationPortal.Data.Entities;

public class Video
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Range(0, 86400)]
    public int Duration { get; set; }

    [Required]
    [StringLength(20)]
    public string Quality { get; set; }

    public int MaterialId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(MaterialId))]
    public virtual Material Material { get; set; }
}
