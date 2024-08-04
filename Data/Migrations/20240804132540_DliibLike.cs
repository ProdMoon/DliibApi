using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DliibApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class DliibLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "Dliibs");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Dliibs");

            migrationBuilder.CreateTable(
                name: "DliibDislikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DliibId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DliibDislikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DliibDislikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DliibDislikes_Dliibs_DliibId",
                        column: x => x.DliibId,
                        principalTable: "Dliibs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DliibLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DliibId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DliibLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DliibLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DliibLikes_Dliibs_DliibId",
                        column: x => x.DliibId,
                        principalTable: "Dliibs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DliibDislikes_DliibId",
                table: "DliibDislikes",
                column: "DliibId");

            migrationBuilder.CreateIndex(
                name: "IX_DliibDislikes_UserId",
                table: "DliibDislikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DliibLikes_DliibId",
                table: "DliibLikes",
                column: "DliibId");

            migrationBuilder.CreateIndex(
                name: "IX_DliibLikes_UserId",
                table: "DliibLikes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DliibDislikes");

            migrationBuilder.DropTable(
                name: "DliibLikes");

            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "Dliibs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Dliibs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
