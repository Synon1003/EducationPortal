using EducationPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationPortal.Data.Configurations
{
    public class MaterialConfigurations : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder
                .HasDiscriminator(m => m.Type)
                .HasValue<Material>("Material")
                .HasValue<Video>("Video")
                .HasValue<Publication>("Publication")
                .HasValue<Article>("Article");
        }
    }
}
