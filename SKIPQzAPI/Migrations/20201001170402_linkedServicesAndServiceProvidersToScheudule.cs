using Microsoft.EntityFrameworkCore.Migrations;

namespace SKIPQzAPI.Migrations
{
    public partial class linkedServicesAndServiceProvidersToScheudule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "Services",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "ServiceProviders",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ScheduleId",
                table: "Services",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviders_ScheduleId",
                table: "ServiceProviders",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceProviders_Schedules_ScheduleId",
                table: "ServiceProviders",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "ScheduleId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Schedules_ScheduleId",
                table: "Services",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "ScheduleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceProviders_Schedules_ScheduleId",
                table: "ServiceProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Schedules_ScheduleId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ScheduleId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_ServiceProviders_ScheduleId",
                table: "ServiceProviders");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "Services",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "ServiceProviders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
