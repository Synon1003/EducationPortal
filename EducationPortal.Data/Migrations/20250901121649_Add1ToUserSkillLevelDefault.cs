using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add1ToUserSkillLevelDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 1, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") },
                column: "Level",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 3, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") },
                column: "Level",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 4, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") },
                column: "Level",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 1, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") },
                column: "Level",
                value: 0);

            migrationBuilder.UpdateData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 3, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") },
                column: "Level",
                value: 0);

            migrationBuilder.UpdateData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 4, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") },
                column: "Level",
                value: 0);
        }
    }
}
