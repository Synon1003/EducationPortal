namespace EducationPortal.Web.Models;

public class ListVideosViewModel
{
    public List<VideoViewModel> Videos { get; set; } = [];
}

public class VideoViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Duration { get; set; }
    public string Quality { get; set; }
    public int MaterialId { get; set; }
}
