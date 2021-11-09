using Microsoft.EntityFrameworkCore.Migrations;

namespace Customer.Persistence.Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "Name" },
                values: new object[,]
                {
                    { 1, "Client 1" },
                    { 2, "Client 2" },
                    { 3, "Client 3" },
                    { 4, "Client 4" },
                    { 5, "Client 5" },
                    { 6, "Client 6" },
                    { 7, "Client 7" },
                    { 8, "Client 8" },
                    { 9, "Client 9" },
                    { 10, "Client 10" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
