using Microsoft.EntityFrameworkCore.Migrations;

namespace SKIPQzAPI.Migrations
{
    public partial class addedBookingIdForeignKeyToExtrasTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Extras",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Extras_BookingId",
                table: "Extras",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Extras_Bookings_BookingId",
                table: "Extras",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Extras_Bookings_BookingId",
                table: "Extras");

            migrationBuilder.DropIndex(
                name: "IX_Extras_BookingId",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Extras");
        }
    }
}
