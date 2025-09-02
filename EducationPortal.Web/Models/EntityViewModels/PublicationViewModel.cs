using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Web.Models;

public class PublicationViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Authors { get; set; }
    public int Pages { get; set; }
    public string Format { get; set; }

    [DisplayName("Publication Year")]
    public int PublicationYear { get; set; }
}

public class PublicationCreateViewModel
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    [StringLength(250)]
    public string Authors { get; set; }

    [Required]
    [Range(0, 9999)]
    public int Pages { get; set; }

    [Required]
    [StringLength(20)]
    public string Format { get; set; }

    [DisplayName("Publication Year")]
    [Required]
    [Range(-1000, 3000)]
    public int PublicationYear { get; set; }
}
