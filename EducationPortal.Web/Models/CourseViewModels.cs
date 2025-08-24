using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Web.Models;

public class ListCoursesViewModel
{
    public List<CourseListViewModel> Courses { get; set; } = [];
    public int TotalCount { get; set; }
}

public class CourseListViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Skills { get; set; } = [];
}

public class CourseDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Skills { get; set; } = [];
    public List<string> Materials { get; set; } = [];
}

public class CourseCreateViewModel
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(100)]
    public string Description { get; set; }

    public List<SkillCreateViewModel> Skills { get; set; } = [];
    public List<VideoCreateViewModel> Videos { get; set; } = [];
    public List<PublicationCreateViewModel> Publications { get; set; } = [];
    public List<ArticleCreateViewModel> Articles { get; set; } = [];
}
