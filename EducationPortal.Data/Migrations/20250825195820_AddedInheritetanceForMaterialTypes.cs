using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EducationPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedInheritetanceForMaterialTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.AddColumn<string>(
                name: "Authors",
                table: "Materials",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Materials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Format",
                table: "Materials",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pages",
                table: "Materials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "PublicationDate",
                table: "Materials",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublicationYear",
                table: "Materials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quality",
                table: "Materials",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResourceLink",
                table: "Materials",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Duration", "Quality" },
                values: new object[] { 120, "1080p" });

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Duration", "Quality" },
                values: new object[] { 90, "720p" });

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PublicationDate", "ResourceLink" },
                values: new object[] { new DateOnly(2022, 5, 26), "https://jasonwatmore.com/post/2022/05/26/vue-3-pinia-jwt-authentication-tutorial-example" });

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PublicationDate", "ResourceLink" },
                values: new object[] { new DateOnly(2022, 5, 26), "https://jasonwatmore.com/vue-3-pinia-jwt-authentication-with-refresh-tokens-example-tutorial" });

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Authors", "Format", "Pages", "PublicationYear" },
                values: new object[] { "Johann Gutenberg", "txt", 1286, 1455 });

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Authors", "Format", "Pages", "PublicationYear" },
                values: new object[] { "Alan Dean Foster, George Lucas", "txt", 272, 1976 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Authors",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Format",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Pages",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "PublicationDate",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "PublicationYear",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Quality",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "ResourceLink",
                table: "Materials");

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    PublicationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ResourceLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Authors = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Format = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Pages = table.Column<int>(type: "int", nullable: false),
                    PublicationYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publications_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Quality = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "MaterialId", "PublicationDate", "ResourceLink" },
                values: new object[,]
                {
                    { 1, 3, new DateOnly(2022, 5, 26), "https://jasonwatmore.com/post/2022/05/26/vue-3-pinia-jwt-authentication-tutorial-example" },
                    { 2, 4, new DateOnly(2022, 5, 26), "https://jasonwatmore.com/vue-3-pinia-jwt-authentication-with-refresh-tokens-example-tutorial" }
                });

            migrationBuilder.InsertData(
                table: "Publications",
                columns: new[] { "Id", "Authors", "Format", "MaterialId", "Pages", "PublicationYear" },
                values: new object[,]
                {
                    { 1, "Johann Gutenberg", "txt", 5, 1286, 1455 },
                    { 2, "Alan Dean Foster, George Lucas", "txt", 6, 272, 1976 }
                });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "Duration", "MaterialId", "Quality" },
                values: new object[,]
                {
                    { 1, 120, 1, "1080p" },
                    { 2, 90, 2, "720p" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_MaterialId",
                table: "Articles",
                column: "MaterialId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publications_MaterialId",
                table: "Publications",
                column: "MaterialId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_MaterialId",
                table: "Videos",
                column: "MaterialId",
                unique: true);
        }
    }
}
