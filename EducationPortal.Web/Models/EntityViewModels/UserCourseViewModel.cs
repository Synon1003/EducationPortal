using System.ComponentModel;

namespace EducationPortal.Web.Models;

public class UserCourseViewModel
{
    public bool IsCompleted { get; set; } = false;

    [DisplayName("Progress")]
    public int ProgressPercentage { get; set; } = 0;
}
