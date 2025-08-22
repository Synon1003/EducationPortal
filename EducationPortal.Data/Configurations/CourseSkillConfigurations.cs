using EducationPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPortal.Data.Configurations
{
    public class CourseSkillConfigurations : IEntityTypeConfiguration<CourseSkill>
    {
        public void Configure(EntityTypeBuilder<CourseSkill> builder)
        {
            builder.HasOne(cs => cs.Course)
                .WithMany(c => c.CourseSkills)
                .HasForeignKey(cs => cs.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cs => cs.Skill)
                .WithMany(c => c.CourseSkills)
                .HasForeignKey(cs => cs.SkillId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
