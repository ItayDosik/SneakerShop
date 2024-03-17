using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SneakerShop.Migrations
{
    /// <inheritdoc />
    public partial class productLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "locationInStore",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "locationInStore",
                table: "Products");
        }
    }
}
