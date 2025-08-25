namespace EducationPortal.Web.Models;

public class ListProfileMaterialViewModel
{
    public List<VideoViewModel> Videos { get; set; } = [];
    public List<PublicationViewModel> Publications { get; set; } = [];
    public List<ArticleViewModel> Articles { get; set; } = [];
}