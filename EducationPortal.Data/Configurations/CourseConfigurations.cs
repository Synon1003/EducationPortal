using EducationPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPortal.Data.Configurations
{
    public class CourseConfigurations : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .HasMany(e => e.Materials)
                .WithMany(e => e.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseMaterials",
                    entity => entity.HasOne<Material>()
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade),
                    entity => entity.HasOne<Course>()
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade),
                    entity =>
                    {
                        entity.HasKey("CourseId", "MaterialId");
                        entity.ToTable("CourseMaterials");
                    }
                );

            builder
                .HasMany(e => e.Skills)
                .WithMany(e => e.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseSkills",
                    entity => entity.HasOne<Skill>()
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade),
                    entity => entity.HasOne<Course>()
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade),
                    entity =>
                    {
                        entity.HasKey("CourseId", "SkillId");
                        entity.ToTable("CourseSkills");
                    }
                );
        }
    }
}
