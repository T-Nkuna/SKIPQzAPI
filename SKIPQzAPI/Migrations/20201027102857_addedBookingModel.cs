using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SKIPQzAPI.Migrations
{
    public partial class addedBookingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(nullable: false),
                    ServiceProviderId = table.Column<int>(nullable: false),
                    BookedDate = table.Column<DateTime>(nullable: false),
                    BookedTimeIntervalTimeComponentIntervalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_TimeComponentIntervals_BookedTimeIntervalTimeComponentIntervalId",
                        column: x => x.BookedTimeIntervalTimeComponentIntervalId,
                        principalTable: "TimeComponentIntervals",
                        principalColumn: "TimeComponentIntervalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookedTimeIntervalTimeComponentIntervalId",
                table: "Bookings",
                column: "BookedTimeIntervalTimeComponentIntervalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
