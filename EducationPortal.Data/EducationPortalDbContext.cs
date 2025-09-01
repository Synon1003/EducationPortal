using Microsoft.EntityFrameworkCore;
using EducationPortal.Data.Entities;
using EducationPortal.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EducationPortal.Data;

public class EducationPortalDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public EducationPortalDbContext(DbContextOptions<EducationPortalDbContext> options) : base(options)
    { }

    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<Skill> Skills { get; set; }
    public virtual DbSet<Material> Materials { get; set; }
    public virtual DbSet<Video> Videos { get; set; }
    public virtual DbSet<Publication> Publications { get; set; }
    public virtual DbSet<Article> Articles { get; set; }
    public virtual DbSet<UserSkill> UserSkills { get; set; }
    public virtual DbSet<UserMaterial> UserMaterials { get; set; }
    public virtual DbSet<UserCourse> UserCourses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CourseConfigurations());
        modelBuilder.ApplyConfiguration(new MaterialConfigurations());
        modelBuilder.ApplyConfiguration(new ApplicationUserConfigurations());

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe"),
                FirstName = "Zo1",
                LastName = "Zo2",
                UserName = "zozo@zo.zo",
                NormalizedUserName = "ZOZO@ZO.ZO",
                Email = "zozo@zo.zo",
                NormalizedEmail = "ZOZO@ZO.ZO",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEG4kPMUMao9yxVsc7yWKoFfd19HOWaBU45bskSnfBLjMQZUTqsvKfjupNn0Ad9SSfQ==",
                SecurityStamp = "P6DH6DQNU6KVOFRNHZZ2KJR3Z3CJHZ4M",
                ConcurrencyStamp = "14ab1c04-7ca7-4b15-b4cf-2f6b7e488a5e",
                LockoutEnabled = true,
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                AccessFailedCount = 0,
                Theme = "business"
            }
        );

        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                Id = 1,
                Name = "Dotnet course",
                Description = "Dotnet course to learn how to create C# Asp.Net Core MVC application",
                CreatedBy = new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe")
            },
            new Course
            {
                Id = 2,
                Name = "Git course",
                Description = "Git course to learn how to use Git repositories",
                CreatedBy = new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe")
            }
        );

        modelBuilder.Entity<UserCourse>().HasData(
            new UserCourse { Id = 1, UserId = new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe"), CourseId = 1, ProgressPercentage = 100 }
        );

        modelBuilder.Entity<Skill>().HasData(
            new Skill { Id = 1, Name = "C#" },
            new Skill { Id = 2, Name = "Git" },
            new Skill { Id = 3, Name = "Sql" },
            new Skill { Id = 4, Name = "Uml" },
            new Skill { Id = 5, Name = "Md" },
            new Skill { Id = 6, Name = "Gitlab" }
        );

        modelBuilder.Entity("CourseSkills").HasData(
            new { CourseId = 1, SkillId = 1 },
            new { CourseId = 2, SkillId = 2 },
            new { CourseId = 1, SkillId = 3 },
            new { CourseId = 1, SkillId = 4 },
            new { CourseId = 2, SkillId = 5 },
            new { CourseId = 2, SkillId = 6 }
        );

        modelBuilder.Entity<UserSkill>().HasData(
            new UserSkill
            {
                UserId = new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe"),
                SkillId = 1,
                Level = 1
            },
            new UserSkill
            {
                UserId = new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe"),
                SkillId = 3,
                Level = 1
            },
            new UserSkill
            {
                UserId = new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe"),
                SkillId = 4,
                Level = 1
            }
        );

        modelBuilder.Entity<Video>().HasData(
            new Video
            {
                Id = 1,
                Title = "Ultimate C# Masterclass",
                Duration = 120,
                Quality = "1080p",
            },
            new Video
            {
                Id = 2,
                Title = "Asp.Net Core",
                Duration = 90,
                Quality = "720p"
            }
        );

        modelBuilder.Entity<Article>().HasData(
            new Article
            {
                Id = 3,
                Title = "Vue 3 + Pinia - JWT Authentication Tutorial & Example",
                PublicationDate = new DateOnly(2022, 5, 26),
                ResourceLink = "https://jasonwatmore.com/post/2022/05/26/vue-3-pinia-jwt-authentication-tutorial-example"
            },
            new Article
            {
                Id = 4,
                Title = "Vue 3 + Pinia - JWT Authentication with Refresh Tokens Example & Tutorial",
                PublicationDate = new DateOnly(2022, 5, 26),
                ResourceLink = "https://jasonwatmore.com/vue-3-pinia-jwt-authentication-with-refresh-tokens-example-tutorial"
            }
        );

        modelBuilder.Entity<Publication>().HasData(
            new Publication
            {
                Id = 5,
                Title = "Gutenberg Bible",
                Format = "txt",
                Authors = "Johann Gutenberg",
                Pages = 1286,
                PublicationYear = 1455
            },
            new Publication
            {
                Id = 6,
                Title = "Star Wars",
                Format = "txt",
                Authors = "Alan Dean Foster, George Lucas",
                Pages = 272,
                PublicationYear = 1976
            }
        );


        modelBuilder.Entity("CourseMaterials").HasData(
            new { CourseId = 1, MaterialId = 1 },
            new { CourseId = 1, MaterialId = 2 },
            new { CourseId = 1, MaterialId = 3 },
            new { CourseId = 1, MaterialId = 4 },
            new { CourseId = 2, MaterialId = 5 },
            new { CourseId = 2, MaterialId = 6 }
        );

        modelBuilder.Entity<UserMaterial>().HasData(
            new UserMaterial { UserId = new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe"), MaterialId = 1 },
            new UserMaterial { UserId = new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe"), MaterialId = 2 },
            new UserMaterial { UserId = new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe"), MaterialId = 3 },
            new UserMaterial { UserId = new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe"), MaterialId = 4 }
        );
    }
}