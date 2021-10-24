using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.Persistence.Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    ProductInStockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.ProductInStockId);
                    table.ForeignKey(
                        name: "FK_Stocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Description for product 1", "Product 1", 383m },
                    { 73, "Description for product 73", "Product 73", 218m },
                    { 72, "Description for product 72", "Product 72", 580m },
                    { 71, "Description for product 71", "Product 71", 943m },
                    { 70, "Description for product 70", "Product 70", 975m },
                    { 69, "Description for product 69", "Product 69", 992m },
                    { 68, "Description for product 68", "Product 68", 511m },
                    { 67, "Description for product 67", "Product 67", 949m },
                    { 66, "Description for product 66", "Product 66", 744m },
                    { 65, "Description for product 65", "Product 65", 649m },
                    { 64, "Description for product 64", "Product 64", 841m },
                    { 63, "Description for product 63", "Product 63", 358m },
                    { 62, "Description for product 62", "Product 62", 480m },
                    { 61, "Description for product 61", "Product 61", 966m },
                    { 60, "Description for product 60", "Product 60", 945m },
                    { 59, "Description for product 59", "Product 59", 563m },
                    { 58, "Description for product 58", "Product 58", 745m },
                    { 57, "Description for product 57", "Product 57", 606m },
                    { 56, "Description for product 56", "Product 56", 590m },
                    { 55, "Description for product 55", "Product 55", 522m },
                    { 54, "Description for product 54", "Product 54", 182m },
                    { 53, "Description for product 53", "Product 53", 212m },
                    { 74, "Description for product 74", "Product 74", 996m },
                    { 52, "Description for product 52", "Product 52", 753m },
                    { 75, "Description for product 75", "Product 75", 994m },
                    { 77, "Description for product 77", "Product 77", 874m },
                    { 98, "Description for product 98", "Product 98", 176m },
                    { 97, "Description for product 97", "Product 97", 520m },
                    { 96, "Description for product 96", "Product 96", 752m },
                    { 95, "Description for product 95", "Product 95", 786m },
                    { 94, "Description for product 94", "Product 94", 591m },
                    { 93, "Description for product 93", "Product 93", 577m },
                    { 92, "Description for product 92", "Product 92", 257m },
                    { 91, "Description for product 91", "Product 91", 113m },
                    { 90, "Description for product 90", "Product 90", 652m },
                    { 89, "Description for product 89", "Product 89", 332m },
                    { 88, "Description for product 88", "Product 88", 951m },
                    { 87, "Description for product 87", "Product 87", 708m },
                    { 86, "Description for product 86", "Product 86", 331m },
                    { 85, "Description for product 85", "Product 85", 167m },
                    { 84, "Description for product 84", "Product 84", 167m },
                    { 83, "Description for product 83", "Product 83", 129m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 82, "Description for product 82", "Product 82", 693m },
                    { 81, "Description for product 81", "Product 81", 289m },
                    { 80, "Description for product 80", "Product 80", 715m },
                    { 79, "Description for product 79", "Product 79", 557m },
                    { 78, "Description for product 78", "Product 78", 528m },
                    { 76, "Description for product 76", "Product 76", 437m },
                    { 51, "Description for product 51", "Product 51", 537m },
                    { 50, "Description for product 50", "Product 50", 368m },
                    { 49, "Description for product 49", "Product 49", 502m },
                    { 22, "Description for product 22", "Product 22", 792m },
                    { 21, "Description for product 21", "Product 21", 216m },
                    { 20, "Description for product 20", "Product 20", 227m },
                    { 19, "Description for product 19", "Product 19", 749m },
                    { 18, "Description for product 18", "Product 18", 654m },
                    { 17, "Description for product 17", "Product 17", 621m },
                    { 16, "Description for product 16", "Product 16", 847m },
                    { 15, "Description for product 15", "Product 15", 596m },
                    { 14, "Description for product 14", "Product 14", 235m },
                    { 13, "Description for product 13", "Product 13", 265m },
                    { 12, "Description for product 12", "Product 12", 983m },
                    { 11, "Description for product 11", "Product 11", 565m },
                    { 10, "Description for product 10", "Product 10", 634m },
                    { 9, "Description for product 9", "Product 9", 446m },
                    { 8, "Description for product 8", "Product 8", 695m },
                    { 7, "Description for product 7", "Product 7", 912m },
                    { 6, "Description for product 6", "Product 6", 960m },
                    { 5, "Description for product 5", "Product 5", 732m },
                    { 4, "Description for product 4", "Product 4", 431m },
                    { 3, "Description for product 3", "Product 3", 595m },
                    { 2, "Description for product 2", "Product 2", 385m },
                    { 23, "Description for product 23", "Product 23", 779m },
                    { 24, "Description for product 24", "Product 24", 849m },
                    { 25, "Description for product 25", "Product 25", 867m },
                    { 26, "Description for product 26", "Product 26", 787m },
                    { 48, "Description for product 48", "Product 48", 401m },
                    { 47, "Description for product 47", "Product 47", 597m },
                    { 46, "Description for product 46", "Product 46", 252m },
                    { 45, "Description for product 45", "Product 45", 392m },
                    { 44, "Description for product 44", "Product 44", 350m },
                    { 43, "Description for product 43", "Product 43", 917m },
                    { 42, "Description for product 42", "Product 42", 779m },
                    { 41, "Description for product 41", "Product 41", 233m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 40, "Description for product 40", "Product 40", 752m },
                    { 39, "Description for product 39", "Product 39", 536m },
                    { 99, "Description for product 99", "Product 99", 701m },
                    { 38, "Description for product 38", "Product 38", 141m },
                    { 36, "Description for product 36", "Product 36", 620m },
                    { 35, "Description for product 35", "Product 35", 242m },
                    { 34, "Description for product 34", "Product 34", 754m },
                    { 33, "Description for product 33", "Product 33", 133m },
                    { 32, "Description for product 32", "Product 32", 820m },
                    { 31, "Description for product 31", "Product 31", 985m },
                    { 30, "Description for product 30", "Product 30", 605m },
                    { 29, "Description for product 29", "Product 29", 436m },
                    { 28, "Description for product 28", "Product 28", 532m },
                    { 27, "Description for product 27", "Product 27", 994m },
                    { 37, "Description for product 37", "Product 37", 903m },
                    { 100, "Description for product 100", "Product 100", 648m }
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "ProductInStockId", "ProductId", "Stock" },
                values: new object[,]
                {
                    { 1, 1, 10 },
                    { 73, 73, 36 },
                    { 72, 72, 28 },
                    { 71, 71, 69 },
                    { 70, 70, 18 },
                    { 69, 69, 68 },
                    { 68, 68, 31 },
                    { 67, 67, 3 },
                    { 66, 66, 74 },
                    { 65, 65, 58 },
                    { 64, 64, 53 },
                    { 63, 63, 52 },
                    { 62, 62, 30 },
                    { 61, 61, 33 },
                    { 60, 60, 40 },
                    { 59, 59, 35 },
                    { 58, 58, 84 },
                    { 57, 57, 72 },
                    { 56, 56, 80 },
                    { 55, 55, 55 },
                    { 54, 54, 55 },
                    { 53, 53, 15 },
                    { 74, 74, 53 },
                    { 52, 52, 40 },
                    { 75, 75, 70 },
                    { 77, 77, 69 },
                    { 98, 98, 7 },
                    { 97, 97, 34 },
                    { 96, 96, 47 },
                    { 95, 95, 63 },
                    { 94, 94, 42 },
                    { 93, 93, 22 },
                    { 92, 92, 38 },
                    { 91, 91, 68 },
                    { 90, 90, 43 },
                    { 89, 89, 82 },
                    { 88, 88, 14 },
                    { 87, 87, 51 },
                    { 86, 86, 76 },
                    { 85, 85, 85 },
                    { 84, 84, 15 },
                    { 83, 83, 77 }
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "ProductInStockId", "ProductId", "Stock" },
                values: new object[,]
                {
                    { 82, 82, 98 },
                    { 81, 81, 42 },
                    { 80, 80, 71 },
                    { 79, 79, 80 },
                    { 78, 78, 77 },
                    { 76, 76, 65 },
                    { 51, 51, 88 },
                    { 50, 50, 4 },
                    { 49, 49, 77 },
                    { 22, 22, 30 },
                    { 21, 21, 52 },
                    { 20, 20, 47 },
                    { 19, 19, 50 },
                    { 18, 18, 19 },
                    { 17, 17, 86 },
                    { 16, 16, 92 },
                    { 15, 15, 59 },
                    { 14, 14, 92 },
                    { 13, 13, 68 },
                    { 12, 12, 73 },
                    { 11, 11, 40 },
                    { 10, 10, 74 },
                    { 9, 9, 27 },
                    { 8, 8, 73 },
                    { 7, 7, 85 },
                    { 6, 6, 59 },
                    { 5, 5, 48 },
                    { 4, 4, 43 },
                    { 3, 3, 26 },
                    { 2, 2, 74 },
                    { 23, 23, 1 },
                    { 24, 24, 41 },
                    { 25, 25, 7 },
                    { 26, 26, 8 },
                    { 48, 48, 28 },
                    { 47, 47, 65 },
                    { 46, 46, 36 },
                    { 45, 45, 61 },
                    { 44, 44, 24 },
                    { 43, 43, 61 },
                    { 42, 42, 86 },
                    { 41, 41, 77 }
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "ProductInStockId", "ProductId", "Stock" },
                values: new object[,]
                {
                    { 40, 40, 97 },
                    { 39, 39, 83 },
                    { 99, 99, 66 },
                    { 38, 38, 58 },
                    { 36, 36, 41 },
                    { 35, 35, 23 },
                    { 34, 34, 37 },
                    { 33, 33, 69 },
                    { 32, 32, 66 },
                    { 31, 31, 16 },
                    { 30, 30, 74 },
                    { 29, 29, 20 },
                    { 28, 28, 55 },
                    { 27, 27, 26 },
                    { 37, 37, 23 },
                    { 100, 100, 86 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                table: "Products",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId",
                table: "Stocks",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
