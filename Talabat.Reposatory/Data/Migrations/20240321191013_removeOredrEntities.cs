using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Reposatory.Data.Migrations
{
    public partial class removeOredrEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "DeliveryMethod");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
