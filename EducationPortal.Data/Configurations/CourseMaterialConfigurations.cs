using EducationPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPortal.Data.Configurations
{
    public class CourseMaterialConfigurations : IEntityTypeConfiguration<CourseMaterial>
    {
        public void Configure(EntityTypeBuilder<CourseMaterial> builder)
        {
            builder.HasOne(cs => cs.Course)
                .WithMany(c => c.CourseMaterials)
                .HasForeignKey(cs => cs.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cs => cs.Material)
                .WithMany(c => c.CourseMaterials)
                .HasForeignKey(cs => cs.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
