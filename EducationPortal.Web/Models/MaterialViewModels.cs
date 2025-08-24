namespace EducationPortal.Web.Models;

public class ListMaterialViewModel
{
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public List<MaterialViewModel> Videos { get; set; } = [];
    public List<MaterialViewModel> Publications { get; set; } = [];
    public List<MaterialViewModel> Articles { get; set; } = [];
}

public class MaterialViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
}

public class MaterialCreateViewModel
{
    public string Title { get; set; }
    public string Type { get; set; }
}