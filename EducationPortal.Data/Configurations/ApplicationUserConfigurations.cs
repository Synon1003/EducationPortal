using EducationPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPortal.Data.Configurations
{
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .HasMany(e => e.Materials)
                .WithMany(e => e.AcquiredByUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserMaterials",
                    entity => entity.HasOne<Material>()
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade),
                    entity => entity.HasOne<ApplicationUser>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade),
                    entity =>
                    {
                        entity.HasKey("UserId", "MaterialId");
                        entity.ToTable("UserMaterials");
                    }
                );

            builder
                .HasMany(e => e.Skills)
                .WithMany(e => e.AcquiredByUsers)
                .UsingEntity<UserSkill>(
                    entity => entity
                        .HasOne(us => us.Skill)
                        .WithMany(s => s.UserSkills)
                        .HasForeignKey(us => us.SkillId),
                    entity => entity
                        .HasOne(us => us.User)
                        .WithMany(u => u.UserSkills)
                        .HasForeignKey(us => us.UserId),
                    entity => entity.ToTable("UserSkills")
                );
        }
    }
}
