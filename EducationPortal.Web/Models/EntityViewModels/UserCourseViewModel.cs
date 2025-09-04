using System.ComponentModel.DataAnnotations;
using EducationPortal.Web.LanguageResources;

namespace EducationPortal.Web.Models;

public class UserCourseViewModel
{
    public bool IsCompleted { get; set; } = false;

    [Display(ResourceType = typeof(Resource), Name = "Progress")]
    public int ProgressPercentage { get; set; } = 0;
}
