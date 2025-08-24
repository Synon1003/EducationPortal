using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EducationPortal.Data.Entities;

public class Publication
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(250)]
    public string Authors { get; set; }

    [Required]
    [Range(0, 9999)]
    public int Pages { get; set; }

    [Required]
    [StringLength(20)]
    public string Format { get; set; }

    [Required]
    [Range(-1000, 3000)]
    public int PublicationYear { get; set; }

    public int MaterialId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(MaterialId))]
    public virtual Material Material { get; set; }
}
