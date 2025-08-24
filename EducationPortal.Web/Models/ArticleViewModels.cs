using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Web.Models;

public class ListArticlesViewModel
{
    public List<ArticleViewModel> Articles { get; set; } = [];
}

public class ArticleViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string PublicationDate { get; set; }
    public string ResourceLink { get; set; }
    public int MaterialId { get; set; }
}

public class ArticleCreateViewModel
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    public DateOnly PublicationDate { get; set; }

    [Required]
    public string ResourceLink { get; set; }
}
