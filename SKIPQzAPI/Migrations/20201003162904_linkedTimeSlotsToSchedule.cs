using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SKIPQzAPI.Migrations
{
    public partial class linkedTimeSlotsToSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recurring",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "TimeSlots");

            migrationBuilder.AddColumn<string>(
                name: "TimeSlotString",
                table: "TimeSlots",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Owner",
                table: "Schedules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Schedules",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSlotString",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Schedules");

            migrationBuilder.AddColumn<bool>(
                name: "Recurring",
                table: "TimeSlots",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "TimeSlots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
