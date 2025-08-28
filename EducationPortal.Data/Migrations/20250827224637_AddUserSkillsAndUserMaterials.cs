using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EducationPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSkillsAndUserMaterials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserMaterials",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMaterials", x => new { x.UserId, x.MaterialId });
                    table.ForeignKey(
                        name: "FK_UserMaterials_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSkills_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserMaterials",
                columns: new[] { "MaterialId", "UserId" },
                values: new object[,]
                {
                    { 1, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") },
                    { 2, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") },
                    { 3, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") },
                    { 4, new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe") }
                });

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
                name: "IX_UserMaterials_MaterialId",
                table: "UserMaterials",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_SkillId",
                table: "UserSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_UserId",
                table: "UserSkills",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMaterials");

            migrationBuilder.DropTable(
                name: "UserSkills");
        }
    }
}
