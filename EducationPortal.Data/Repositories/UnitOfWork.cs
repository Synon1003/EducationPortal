namespace EducationPortal.Data.Repositories.Interfaces;

public class UnitOfWork : IUnitOfWork
{
    private readonly EducationPortalDbContext _context;

    private ICourseRepository _courseRepository;
    private IUserCourseRepository _userCourseRepository;
    private IUserMaterialRepository _userMaterialRepository;
    private IUserSkillRepository _userSkillRepository;
    private ISkillRepository _skillRepository;
    private IMaterialRepository _materialRepository;

    public UnitOfWork(
        EducationPortalDbContext context,
        ICourseRepository courseRepository,
        IUserCourseRepository userCourseRepository,
        IUserMaterialRepository userMaterialRepository,
        IUserSkillRepository userSkillRepository,
        ISkillRepository skillRepository,
        IMaterialRepository materialRepository
    )
    {
        _context = context;
        _courseRepository = courseRepository;
        _userCourseRepository = userCourseRepository;
        _userMaterialRepository = userMaterialRepository;
        _userSkillRepository = userSkillRepository;
        _skillRepository = skillRepository;
        _materialRepository = materialRepository;
    }

    public ICourseRepository CourseRepository => _courseRepository;
    public IUserCourseRepository UserCourseRepository => _userCourseRepository;
    public IUserMaterialRepository UserMaterialRepository => _userMaterialRepository;
    public IUserSkillRepository UserSkillRepository => _userSkillRepository;
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
