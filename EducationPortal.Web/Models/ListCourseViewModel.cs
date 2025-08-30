namespace EducationPortal.Web.Models;

public class ListCoursesViewModel
{
    public List<CourseListViewModel> Courses { get; set; } = [];
    public int TotalCount { get; set; }
    public string TotalCountText => TotalCount == 1 ?
        $"{TotalCount}  {(CurrentOption == "all" ? "registered" : CurrentOption)} course" :
        $"{TotalCount}  {(CurrentOption == "all" ? "registered" : CurrentOption)} courses";

    public Dictionary<string, string> SelectOptions { get; set; } =
        new Dictionary<string, string>()
        {
            { "All", "all" },
            { "Available", "available" },
            { "In-progress", "in-progress" },
            { "Completed", "completed" },
            { "Created", "created" },
        };
    public string CurrentOption { get; set; } = String.Empty;
}