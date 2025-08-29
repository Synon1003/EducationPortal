using System.ComponentModel.DataAnnotations;

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

public class CourseListViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Skills { get; set; } = [];
    public string CreatedBy { get; set; }
    public UserCourseViewModel? UserCourse { get; set; }
}

public class CourseDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Skills { get; set; } = [];
    public List<string> Materials { get; set; } = [];
    public string CreatedBy { get; set; }
    public UserCourseViewModel? UserCourse { get; set; }
}

public class CourseCreateViewModel
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(250)]
    public string Description { get; set; }

    public List<SkillCreateViewModel> Skills { get; set; } = [];
    public List<VideoCreateViewModel> Videos { get; set; } = [];
    public List<PublicationCreateViewModel> Publications { get; set; } = [];
    public List<ArticleCreateViewModel> Articles { get; set; } = [];
}
