using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EducationPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReSeedCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseMaterials",
                keyColumns: new[] { "CourseId", "MaterialId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "CourseMaterials",
                keyColumns: new[] { "CourseId", "MaterialId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "CourseMaterials",
                keyColumns: new[] { "CourseId", "MaterialId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "CourseMaterials",
                keyColumns: new[] { "CourseId", "MaterialId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "CourseSkills",
                keyColumns: new[] { "CourseId", "SkillId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "UserMaterials",
                keyColumns: new[] { "MaterialId", "UserId" },
                keyValues: new object[] { 3, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") });

            migrationBuilder.DeleteData(
                table: "UserMaterials",
                keyColumns: new[] { "MaterialId", "UserId" },
                keyValues: new object[] { 4, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") });

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.InsertData(
                table: "CourseSkills",
                columns: new[] { "CourseId", "SkillId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 6 }
                });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { ".Net course to learn how to create C# Asp.Net Core MVC application", ".Net course" });

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 1,
                column: "Duration",
                value: 5200);

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Duration", "Title" },
                values: new object[] { 2900, "Asp.Net Core MVC" });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "Duration", "Quality", "Title", "Type" },
                values: new object[] { 3, 1000, "720p", "The Git & Github Bootcamp", "Video" });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "Authors", "Format", "Pages", "PublicationYear", "Title", "Type" },
                values: new object[] { 4, "Scott Chacon, Ben Straub", "pdf", 501, 2014, "ProGit", "Publication" });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "MVC");

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: ".Net");

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Github");

            migrationBuilder.InsertData(
                table: "UserSkills",
                columns: new[] { "SkillId", "UserId", "Level" },
                values: new object[,]
                {
                    { 2, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe"), 1 },
                    { 6, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe"), 1 }
                });

            migrationBuilder.InsertData(
                table: "CourseMaterials",
                columns: new[] { "CourseId", "MaterialId" },
                values: new object[,]
                {
                    { 2, 3 },
                    { 2, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseMaterials",
                keyColumns: new[] { "CourseId", "MaterialId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "CourseMaterials",
                keyColumns: new[] { "CourseId", "MaterialId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "CourseSkills",
                keyColumns: new[] { "CourseId", "SkillId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "CourseSkills",
                keyColumns: new[] { "CourseId", "SkillId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 2, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") });

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 6, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") });

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "CourseSkills",
                columns: new[] { "CourseId", "SkillId" },
                values: new object[] { 2, 6 });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Dotnet course to learn how to create C# Asp.Net Core MVC application", "Dotnet course" });

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 1,
                column: "Duration",
                value: 120);

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Duration", "Title" },
                values: new object[] { 90, "Asp.Net Core" });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "PublicationDate", "ResourceLink", "Title", "Type" },
                values: new object[,]
                {
                    { 3, new DateOnly(2022, 5, 26), "https://jasonwatmore.com/post/2022/05/26/vue-3-pinia-jwt-authentication-tutorial-example", "Vue 3 + Pinia - JWT Authentication Tutorial & Example", "Article" },
                    { 4, new DateOnly(2022, 5, 26), "https://jasonwatmore.com/vue-3-pinia-jwt-authentication-with-refresh-tokens-example-tutorial", "Vue 3 + Pinia - JWT Authentication with Refresh Tokens Example & Tutorial", "Article" }
                });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "Authors", "Format", "Pages", "PublicationYear", "Title", "Type" },
                values: new object[,]
                {
                    { 5, "Johann Gutenberg", "txt", 1286, 1455, "Gutenberg Bible", "Publication" },
                    { 6, "Alan Dean Foster, George Lucas", "txt", 272, 1976, "Star Wars", "Publication" }
                });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Sql");

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Uml");

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Md");

            migrationBuilder.InsertData(
                table: "CourseMaterials",
                columns: new[] { "CourseId", "MaterialId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 5 },
                    { 2, 6 }
                });

            migrationBuilder.InsertData(
                table: "UserMaterials",
                columns: new[] { "MaterialId", "UserId" },
                values: new object[,]
                {
                    { 3, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") },
                    { 4, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") }
                });
        }
    }
}
