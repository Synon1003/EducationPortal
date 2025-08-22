using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedCourseJoinTablesForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseMaterials_Materials_CourseId",
                table: "CourseMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSkills_Skills_CourseId",
                table: "CourseSkills");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSkills_SkillId",
                table: "CourseSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseMaterials_MaterialId",
                table: "CourseMaterials",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseMaterials_Materials_MaterialId",
                table: "CourseMaterials",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSkills_Skills_SkillId",
                table: "CourseSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseMaterials_Materials_MaterialId",
                table: "CourseMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSkills_Skills_SkillId",
                table: "CourseSkills");

            migrationBuilder.DropIndex(
                name: "IX_CourseSkills_SkillId",
                table: "CourseSkills");

            migrationBuilder.DropIndex(
                name: "IX_CourseMaterials_MaterialId",
                table: "CourseMaterials");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseMaterials_Materials_CourseId",
                table: "CourseMaterials",
                column: "CourseId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSkills_Skills_CourseId",
                table: "CourseSkills",
                column: "CourseId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
