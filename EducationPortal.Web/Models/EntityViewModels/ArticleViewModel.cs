using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Web.Models;

public class ArticleViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }

    [DisplayName("Publication Date")]
    public DateOnly PublicationDate { get; set; }

    [DisplayName("Resourse Link")]
    public string ResourceLink { get; set; }
}

public class ArticleCreateViewModel
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [DisplayName("Publication Date")]
    [Required]
    [DataType(DataType.Date)]
    public DateOnly PublicationDate { get; set; }

    [DisplayName("Resourse Link")]
    [Required]
    [StringLength(500)]
    public string ResourceLink { get; set; }
}
