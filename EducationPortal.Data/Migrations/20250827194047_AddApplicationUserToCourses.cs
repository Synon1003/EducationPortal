using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUserToCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Theme", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe"), 0, "14ab1c04-7ca7-4b15-b4cf-2f6b7e488a5e", "zozo@zo.zo", true, "Zo1", "Zo2", true, null, "ZOZO@ZO.ZO", "ZOZO@ZO.ZO", "AQAAAAIAAYagAAAAEG4kPMUMao9yxVsc7yWKoFfd19HOWaBU45bskSnfBLjMQZUTqsvKfjupNn0Ad9SSfQ==", null, false, "P6DH6DQNU6KVOFRNHZZ2KJR3Z3CJHZ4M", "business", false, "zozo@zo.zo" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedBy",
                value: new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe"));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedBy",
                value: new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe"));

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CreatedBy",
                table: "Courses",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_CreatedBy",
                table: "Courses",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_CreatedBy",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CreatedBy",
                table: "Courses");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2fc3ecef-00ee-4aa3-f194-08dde5627abe"));

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Courses");
        }
    }
}
