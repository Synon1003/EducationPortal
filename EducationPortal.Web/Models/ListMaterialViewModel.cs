namespace EducationPortal.Web.Models;

public class ListMaterialViewModel
{
    public int CourseId { get; set; }
    public string CourseName { get; set; } = "";
    public List<MaterialViewModel> Videos { get; set; } = [];
    public List<MaterialViewModel> Publications { get; set; } = [];
    public List<MaterialViewModel> Articles { get; set; } = [];
    public int MaterialsTotal { get; set; }
    public int MaterialsDone { get; set; }
}