using Microsoft.EntityFrameworkCore.Migrations;

namespace SKIPQzAPI.Migrations
{
    public partial class AddedCostColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Bookings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "clientId",
                table: "Bookings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_clientId",
                table: "Bookings",
                column: "clientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_clientId",
                table: "Bookings",
                column: "clientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_clientId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_clientId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "clientId",
                table: "Bookings");
        }
    }
}
