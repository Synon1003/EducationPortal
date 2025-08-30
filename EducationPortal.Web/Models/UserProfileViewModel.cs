namespace EducationPortal.Web.Models;

public class UserProfileViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }

    public List<VideoViewModel> Videos { get; set; } = [];
    public List<PublicationViewModel> Publications { get; set; } = [];
    public List<ArticleViewModel> Articles { get; set; } = [];
}
