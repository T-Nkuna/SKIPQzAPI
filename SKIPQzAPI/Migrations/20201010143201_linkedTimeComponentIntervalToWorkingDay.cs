using Microsoft.EntityFrameworkCore.Migrations;

namespace SKIPQzAPI.Migrations
{
    public partial class linkedTimeComponentIntervalToWorkingDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeComponentIntervals_WorkingDay_WorkingDayId",
                table: "TimeComponentIntervals");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingDay_ServiceProviders_ServiceProviderId",
                table: "WorkingDay");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceProviderId",
                table: "WorkingDay",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WorkingDayId",
                table: "TimeComponentIntervals",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeComponentIntervals_WorkingDay_WorkingDayId",
                table: "TimeComponentIntervals",
                column: "WorkingDayId",
                principalTable: "WorkingDay",
                principalColumn: "WorkingDayId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingDay_ServiceProviders_ServiceProviderId",
                table: "WorkingDay",
                column: "ServiceProviderId",
                principalTable: "ServiceProviders",
                principalColumn: "ServiceProviderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeComponentIntervals_WorkingDay_WorkingDayId",
                table: "TimeComponentIntervals");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingDay_ServiceProviders_ServiceProviderId",
                table: "WorkingDay");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceProviderId",
                table: "WorkingDay",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "WorkingDayId",
                table: "TimeComponentIntervals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_TimeComponentIntervals_WorkingDay_WorkingDayId",
                table: "TimeComponentIntervals",
                column: "WorkingDayId",
                principalTable: "WorkingDay",
                principalColumn: "WorkingDayId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingDay_ServiceProviders_ServiceProviderId",
                table: "WorkingDay",
                column: "ServiceProviderId",
                principalTable: "ServiceProviders",
                principalColumn: "ServiceProviderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
