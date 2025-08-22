using Microsoft.EntityFrameworkCore;
using EducationPortal.Data.Entities;
using EducationPortal.Data.Configurations;

namespace EducationPortal.Data;

public class EducationPortalDbContext : DbContext
{
    public EducationPortalDbContext(DbContextOptions<EducationPortalDbContext> options) : base(options)
    { }

    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<Skill> Skills { get; set; }
    public virtual DbSet<CourseSkill> CourseSkills { get; set; }
    public virtual DbSet<Material> Materials { get; set; }
    public virtual DbSet<Video> Videos { get; set; }
    public virtual DbSet<Publication> Publications { get; set; }
    public virtual DbSet<Article> Articles { get; set; }
    public virtual DbSet<CourseMaterial> CourseMaterials { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CourseSkillConfigurations());
        modelBuilder.ApplyConfiguration(new CourseMaterialConfigurations());
        modelBuilder.ApplyConfiguration(new MaterialConfigurations());

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Skill>().HasData(
            new Skill { Id = 1, Name = "C#" },
            new Skill { Id = 2, Name = "Git" }
        );

        modelBuilder.Entity<Material>().HasData(
            new Material { Id = 1, Title = "Ultimate C# Masterclass", Type = "Video" },
            new Material { Id = 2, Title = "Asp.Net Core", Type = "Video" },
            new Material { Id = 3, Title = "Vue 3 + Pinia - JWT Authentication Tutorial & Example", Type = "Article" }
        );

        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Name = "Dotnet course", Description = "Dotnet course to learn how to create C# Asp.Net Core MVC application" },
            new Course { Id = 2, Name = "Git course", Description = "Git course to learn how to use Git repositories" }
        );

        modelBuilder.Entity<CourseSkill>().HasData(
            new CourseSkill { Id = 1, CourseId = 1, SkillId = 1 },
            new CourseSkill { Id = 2, CourseId = 2, SkillId = 2 }
        );

        modelBuilder.Entity<CourseMaterial>().HasData(
            new CourseMaterial { Id = 1, CourseId = 1, MaterialId = 1 },
            new CourseMaterial { Id = 2, CourseId = 1, MaterialId = 2 }
        );

        modelBuilder.Entity<Video>().HasData(
            new Video { Id = 1, MaterialId = 1, Duration = 120, Quality = "1080p" },
            new Video { Id = 2, MaterialId = 2, Duration = 90, Quality = "720p" }
        );

        modelBuilder.Entity<Article>().HasData(
            new Article { Id = 1, MaterialId = 1, PublicationDate = new DateOnly(2022, 5, 26), ResourceLink = "https://jasonwatmore.com/post/2022/05/26/vue-3-pinia-jwt-authentication-tutorial-example" }
        );
    }
}