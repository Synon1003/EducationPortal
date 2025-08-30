using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Web.Models;

public class ArticleViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string PublicationDate { get; set; }
    public string ResourceLink { get; set; }
}

public class ArticleCreateViewModel
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateOnly PublicationDate { get; set; }

    [Required]
    [StringLength(500)]
    public string ResourceLink { get; set; }
}
