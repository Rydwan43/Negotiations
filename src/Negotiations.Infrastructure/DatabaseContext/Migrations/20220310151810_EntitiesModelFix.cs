using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Negotiations.Infrastructure.DatabaseContext.Migrations
{
    public partial class EntitiesModelFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_CreatedByID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CreatedByID",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedByID",
                table: "Products",
                column: "CreatedByID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CreatedByID",
                table: "Products",
                column: "CreatedByID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
