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
                .UsingEntity<CourseMaterial>();

            builder
                .HasMany(e => e.Skills)
                .WithMany(e => e.Courses)
                .UsingEntity<CourseSkill>();
        }
    }
}
