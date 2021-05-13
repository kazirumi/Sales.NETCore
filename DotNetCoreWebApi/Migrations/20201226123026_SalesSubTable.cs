using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCoreWebApi.Migrations
{
    public partial class SalesSubTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesSubs",
                columns: table => new
                {
                    SalesSubID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SalesMainID = table.Column<int>(nullable: false),
                    ItemID = table.Column<int>(nullable: false),
                    ItemQuantity = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesSubs", x => x.SalesSubID);
                    table.ForeignKey(
                        name: "FK_SalesSubs_items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "items",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesSubs_SalesMains_SalesMainID",
                        column: x => x.SalesMainID,
                        principalTable: "SalesMains",
                        principalColumn: "SalesMainID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesSubs_ItemID",
                table: "SalesSubs",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesSubs_SalesMainID",
                table: "SalesSubs",
                column: "SalesMainID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesSubs");
        }
    }
}
