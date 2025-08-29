namespace EducationPortal.Data.Repositories.Interfaces;

public class UnitOfWork : IUnitOfWork
{
    private readonly EducationPortalDbContext _context;

    private ICourseRepository _courseRepository;
    private IUserCourseRepository _userCourseRepository;
    private ISkillRepository _skillRepository;
    private IMaterialRepository _materialRepository;

    public UnitOfWork(
        EducationPortalDbContext context,
        ICourseRepository courseRepository,
        IUserCourseRepository userCourseRepository,
        ISkillRepository skillRepository,
        IMaterialRepository materialRepository
    )
    {
        _context = context;
        _courseRepository = courseRepository;
        _userCourseRepository = userCourseRepository;
        _skillRepository = skillRepository;
        _materialRepository = materialRepository;
    }

    public ICourseRepository CourseRepository => _courseRepository;
    public IUserCourseRepository UserCourseRepository => _userCourseRepository;
    public ISkillRepository SkillRepository => _skillRepository;
    public IMaterialRepository MaterialRepository => _materialRepository;

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
