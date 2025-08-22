using EducationPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPortal.Data.Configurations
{
    public class MaterialConfigurations : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasOne(m => m.Video)
                .WithOne(v => v.Material)
                .HasForeignKey<Video>(v => v.MaterialId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Publication)
                .WithOne(p => p.Material)
                .HasForeignKey<Publication>(p => p.MaterialId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Article)
                .WithOne(a => a.Material)
                .HasForeignKey<Article>(a => a.MaterialId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
