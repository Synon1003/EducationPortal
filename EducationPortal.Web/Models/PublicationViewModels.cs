namespace EducationPortal.Web.Models;

public class ListPublicationsViewModel
{
    public List<PublicationViewModel> Publications { get; set; } = [];
}

public class PublicationViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Authors { get; set; }
    public int Pages { get; set; }
    public string Format { get; set; }
    public int PublicationYear { get; set; }
    public int MaterialId { get; set; }
}
