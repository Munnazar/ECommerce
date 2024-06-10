using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sample.Data.Migrations
{
    /// <inheritdoc />
    public partial class qq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CatageryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CatageryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CatageryId",
                table: "Products",
                column: "CatageryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CatageryId",
                table: "Products",
                column: "CatageryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
