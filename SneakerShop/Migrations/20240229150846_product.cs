using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SneakerShop.Migrations
{
    /// <inheritdoc />
    public partial class product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductPictureURL1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPictureURL2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPictureURL3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qnt = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOnSale = table.Column<bool>(type: "bit", nullable: false), 
                    SalePercentage = table.Column<int>(type: "int", nullable: true) 

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
