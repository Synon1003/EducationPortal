using Microsoft.EntityFrameworkCore;
using EducationPortal.Data.Entities;

namespace EducationPortal.Data;

public class EducationPortalDbContext : DbContext
{
    public EducationPortalDbContext(DbContextOptions<EducationPortalDbContext> options) : base(options)
    { }

    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<Skill> Skills { get; set; }
    public virtual DbSet<CourseSkill> CourseSkills { get; set; }
    public virtual DbSet<Material> Materials { get; set; }
    public virtual DbSet<CourseMaterial> CourseMaterials { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseSkill>()
            .HasOne(cs => cs.Course)
            .WithMany(c => c.CourseSkills)
            .HasForeignKey(cs => cs.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CourseSkill>()
            .HasOne(cs => cs.Skill)
            .WithMany(c => c.CourseSkills)
            .HasForeignKey(cs => cs.SkillId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CourseMaterial>()
            .HasOne(cs => cs.Course)
            .WithMany(c => c.CourseMaterials)
            .HasForeignKey(cs => cs.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CourseMaterial>()
            .HasOne(cs => cs.Material)
            .WithMany(c => c.CourseMaterials)
            .HasForeignKey(cs => cs.MaterialId)
            .OnDelete(DeleteBehavior.Restrict);

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
            new Material { Id = 2, Title = "Asp.Net Core", Type = "Video" }
        );

        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Name = "Dotnet course", Description = "Dotnet course to learn how to create C# Asp.Net Core MVC application" },
            new Course { Id = 2, Name = "Git course", Description = "Git course to learn how to use Git repositories" }
        );

        modelBuilder.Entity<CourseSkill>().HasData(
            new { Id = 1, CourseId = 1, SkillId = 1 },
            new { Id = 2, CourseId = 2, SkillId = 2 }
        );

        modelBuilder.Entity<CourseMaterial>().HasData(
            new { Id = 1, CourseId = 1, MaterialId = 1 },
            new { Id = 2, CourseId = 1, MaterialId = 2 }
        );
    }
}