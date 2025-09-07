using Moq;

using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;

namespace EducationPortal.Tests.Mocks;

public static class MockSkillRepository
{
    public static Mock<ISkillRepository> GetSkillRepository()
    {
        var skills = new List<Skill>
        {
            new Skill { Id = 1, Name = "Skill1" },
            new Skill { Id = 2, Name = "Skill2" }
        };

        var mockRepository = new Mock<ISkillRepository>();

        mockRepository.Setup(r => r.Exists(It.IsAny<Func<Skill, bool>>()))
            .Returns((Func<Skill, bool> predicate) => predicate(new Skill { Name = "ExistingSkillName" }));

        mockRepository.Setup(u => u.GetByIdAsync(1))
            .ReturnsAsync(new Skill { Id = 1, Name = "LoadedSkill" });

        mockRepository.Setup(u => u.GetSkillsByCourseIdAsync(
            It.IsAny<int>())).ReturnsAsync(skills);

        return mockRepository;
    }
}