using Microsoft.EntityFrameworkCore.Migrations;

namespace SKIPQzAPI.Migrations
{
    public partial class addedWorkingDaysTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeComponentIntervals_WorkingDay_WorkingDayId",
                table: "TimeComponentIntervals");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingDay_ServiceProviders_ServiceProviderId",
                table: "WorkingDay");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingDay",
                table: "WorkingDay");

            migrationBuilder.RenameTable(
                name: "WorkingDay",
                newName: "WorkingDays");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingDay_ServiceProviderId",
                table: "WorkingDays",
                newName: "IX_WorkingDays_ServiceProviderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingDays",
                table: "WorkingDays",
                column: "WorkingDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeComponentIntervals_WorkingDays_WorkingDayId",
                table: "TimeComponentIntervals",
                column: "WorkingDayId",
                principalTable: "WorkingDays",
                principalColumn: "WorkingDayId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingDays_ServiceProviders_ServiceProviderId",
                table: "WorkingDays",
                column: "ServiceProviderId",
                principalTable: "ServiceProviders",
                principalColumn: "ServiceProviderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeComponentIntervals_WorkingDays_WorkingDayId",
                table: "TimeComponentIntervals");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingDays_ServiceProviders_ServiceProviderId",
                table: "WorkingDays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingDays",
                table: "WorkingDays");

            migrationBuilder.RenameTable(
                name: "WorkingDays",
                newName: "WorkingDay");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingDays_ServiceProviderId",
                table: "WorkingDay",
                newName: "IX_WorkingDay_ServiceProviderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingDay",
                table: "WorkingDay",
                column: "WorkingDayId");

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
    }
}
