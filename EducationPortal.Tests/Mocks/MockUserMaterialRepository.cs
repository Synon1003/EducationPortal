using Moq;

using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;
using System.Linq.Expressions;

namespace EducationPortal.Tests.Mocks;

public static class MockUserMaterialRepository
{
    public static Mock<IUserMaterialRepository> GetUserMaterialRepository()
    {
        var userMaterial = new UserMaterial
        {
            UserId = new Guid(),
            MaterialId = 1,
        };

        var mockRepository = new Mock<IUserMaterialRepository>();

        mockRepository.Setup(u => u.GetAllByUserIdAsync(It.IsAny<Guid>()))
        .ReturnsAsync([userMaterial]);

        mockRepository.Setup(uc => uc.GetByFilterAsync(It.Is<Expression<Func<UserMaterial, bool>>>(
            expr => expr.Compile().Invoke(new UserMaterial
            {
                UserId = userMaterial.UserId,
                MaterialId = userMaterial.MaterialId
            })
        ))).ReturnsAsync(userMaterial);

        mockRepository.Setup(r => r.Exists(It.IsAny<Func<UserMaterial, bool>>())
        ).Returns((Func<UserMaterial, bool> predicate) =>
        {
            return predicate(new UserMaterial { UserId = Guid.Empty, MaterialId = 1 });
        });

        return mockRepository;
    }
}