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
