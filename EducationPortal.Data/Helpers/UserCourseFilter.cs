namespace EducationPortal.Data.Helpers;

public class UserCoursesFilter
{
    public Guid? UserId { get; set; }
    public int? MaterialId { get; set; }
    public bool? IsCompleted { get; set; }
}
