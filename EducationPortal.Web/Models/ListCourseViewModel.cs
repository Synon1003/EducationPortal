namespace EducationPortal.Web.Models;

public class ListCoursesViewModel
{
    public List<CourseListViewModel> Courses { get; set; } = [];
    public int TotalCount { get; set; }
    public string TotalCountText =>
        $"{(CurrentOption == "all" || CurrentOption is null ? "registered" : CurrentOption)}Courses";

    public Dictionary<string, string> SelectOptions { get; set; } =
        new Dictionary<string, string>()
        {
            { "All", "all" },
            { "Available", "available" },
            { "InProgress", "inprogress" },
            { "Completed", "completed" },
            { "Created", "created" },
        };
    public string CurrentOption { get; set; } = String.Empty;
}