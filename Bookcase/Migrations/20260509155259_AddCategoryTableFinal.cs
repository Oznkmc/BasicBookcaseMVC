using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookcase.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryTableFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Kitaplar");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Kitaplar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kitaplar_CategoryId",
                table: "Kitaplar",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kitaplar_Kategoriler_CategoryId",
                table: "Kitaplar",
                column: "CategoryId",
                principalTable: "Kategoriler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kitaplar_Kategoriler_CategoryId",
                table: "Kitaplar");

            migrationBuilder.DropTable(
                name: "Kategoriler");

            migrationBuilder.DropIndex(
                name: "IX_Kitaplar_CategoryId",
                table: "Kitaplar");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Kitaplar");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Kitaplar",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
