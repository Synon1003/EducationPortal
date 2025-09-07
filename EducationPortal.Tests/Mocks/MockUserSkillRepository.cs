using Moq;

using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;
using System.Linq.Expressions;

namespace EducationPortal.Tests.Mocks;

public static class MockUserSkillRepository
{
    public static Mock<IUserSkillRepository> GetUserSkillRepository()
    {
        var userSkill = new UserSkill
        {
            UserId = new Guid(),
            SkillId = 1,
        };

        var mockRepository = new Mock<IUserSkillRepository>();

        mockRepository.Setup(uc => uc.GetByFilterAsync(It.Is<Expression<Func<UserSkill, bool>>>(
            expr => expr.Compile().Invoke(new UserSkill
            {
                UserId = userSkill.UserId,
                SkillId = userSkill.SkillId
            })
        ))).ReturnsAsync(userSkill);

        return mockRepository;
    }
}