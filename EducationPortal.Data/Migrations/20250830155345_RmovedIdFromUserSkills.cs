using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EducationPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class RmovedIdFromUserSkills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSkills",
                table: "UserSkills");

            migrationBuilder.DropIndex(
                name: "IX_UserSkills_UserId",
                table: "UserSkills");

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserSkills");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSkills",
                table: "UserSkills",
                columns: new[] { "UserId", "SkillId" });

            migrationBuilder.InsertData(
                table: "UserSkills",
                columns: new[] { "SkillId", "UserId", "Level" },
                values: new object[,]
                {
                    { 1, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe"), 0 },
                    { 3, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe"), 0 },
                    { 4, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe"), 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSkills",
                table: "UserSkills");

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 1, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") });

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 3, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") });

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 4, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") });

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserSkills",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSkills",
                table: "UserSkills",
                column: "Id");

            migrationBuilder.InsertData(
                table: "UserSkills",
                columns: new[] { "Id", "Level", "SkillId", "UserId" },
                values: new object[,]
                {
                    { 1, 0, 1, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") },
                    { 2, 0, 3, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") },
                    { 3, 0, 4, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_UserId",
                table: "UserSkills",
                column: "UserId");
        }
    }
}
