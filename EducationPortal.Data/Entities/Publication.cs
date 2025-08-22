using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EducationPortal.Data.Entities;

public class Publication
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Authors { get; set; }

    [Required]
    public int Pages { get; set; }

    [Required]
    [StringLength(20)]
    public string Format { get; set; }

    [Required]
    public int PublicationYear { get; set; }

    public int MaterialId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(MaterialId))]
    public virtual Material Material { get; set; }
}
