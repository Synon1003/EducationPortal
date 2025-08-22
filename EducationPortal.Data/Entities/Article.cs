using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EducationPortal.Data.Entities;

public class Article
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateOnly PublicationDate { get; set; }

    [Required]
    public string ResourceLink { get; set; }

    public int MaterialId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(MaterialId))]
    public virtual Material Material { get; set; }
}
