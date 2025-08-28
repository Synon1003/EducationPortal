using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EducationPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedCourseSkillsAndCourseMaterialsToJoinTablesOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseSkills",
                table: "CourseSkills");

            migrationBuilder.DropIndex(
                name: "IX_CourseSkills_CourseId",
                table: "CourseSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseMaterials",
                table: "CourseMaterials");

            migrationBuilder.DropIndex(
                name: "IX_CourseMaterials_CourseId",
                table: "CourseMaterials");

            migrationBuilder.DeleteData(
                table: "CourseMaterials",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CourseMaterials",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CourseMaterials",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CourseMaterials",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CourseMaterials",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CourseMaterials",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CourseSkills",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CourseSkills",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CourseSkills",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CourseSkills",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CourseSkills",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CourseSkills",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CourseSkills");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CourseMaterials");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseSkills",
                table: "CourseSkills",
                columns: new[] { "CourseId", "SkillId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseMaterials",
                table: "CourseMaterials",
                columns: new[] { "CourseId", "MaterialId" });

            migrationBuilder.InsertData(
                table: "CourseMaterials",
                columns: new[] { "CourseId", "MaterialId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 5 },
                    { 2, 6 }
                });

            migrationBuilder.InsertData(
                table: "CourseSkills",
                columns: new[] { "CourseId", "SkillId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 2 },
                    { 2, 5 },
                    { 2, 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseSkills",
                table: "CourseSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseMaterials",
                table: "CourseMaterials");

            migrationBuilder.DeleteData(
                table: "CourseMaterials",
                keyColumns: new[] { "CourseId", "MaterialId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "CourseMaterials",
                keyColumns: new[] { "CourseId", "MaterialId" },
                keyValues: new object[] { 1, 2 });

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
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "CourseSkills",
                keyColumns: new[] { "CourseId", "SkillId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "CourseSkills",
                keyColumns: new[] { "CourseId", "SkillId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "CourseSkills",
                keyColumns: new[] { "CourseId", "SkillId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "CourseSkills",
                keyColumns: new[] { "CourseId", "SkillId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "CourseSkills",
                keyColumns: new[] { "CourseId", "SkillId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CourseSkills",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CourseMaterials",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseSkills",
                table: "CourseSkills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseMaterials",
                table: "CourseMaterials",
                column: "Id");

            migrationBuilder.InsertData(
                table: "CourseMaterials",
                columns: new[] { "Id", "CourseId", "MaterialId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 1, 4 },
                    { 5, 2, 5 },
                    { 6, 2, 6 }
                });

            migrationBuilder.InsertData(
                table: "CourseSkills",
                columns: new[] { "Id", "CourseId", "SkillId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 1, 3 },
                    { 4, 1, 4 },
                    { 5, 2, 5 },
                    { 6, 2, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseSkills_CourseId",
                table: "CourseSkills",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseMaterials_CourseId",
                table: "CourseMaterials",
                column: "CourseId");
        }
    }
}
