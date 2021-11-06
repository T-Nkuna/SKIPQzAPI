using Microsoft.EntityFrameworkCore.Migrations;

namespace SKIPQzAPI.Migrations
{
    public partial class ChangedServiceProviderIdType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingDays_ServiceProviders_ServiceProviderId1",
                table: "WorkingDays");

            migrationBuilder.DropIndex(
                name: "IX_WorkingDays_ServiceProviderId1",
                table: "WorkingDays");

            migrationBuilder.DropColumn(
                name: "ServiceProviderId1",
                table: "WorkingDays");

            migrationBuilder.AlterColumn<long>(
                name: "ServiceProviderId",
                table: "WorkingDays",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDays_ServiceProviderId",
                table: "WorkingDays",
                column: "ServiceProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingDays_ServiceProviders_ServiceProviderId",
                table: "WorkingDays",
                column: "ServiceProviderId",
                principalTable: "ServiceProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingDays_ServiceProviders_ServiceProviderId",
                table: "WorkingDays");

            migrationBuilder.DropIndex(
                name: "IX_WorkingDays_ServiceProviderId",
                table: "WorkingDays");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceProviderId",
                table: "WorkingDays",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ServiceProviderId1",
                table: "WorkingDays",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDays_ServiceProviderId1",
                table: "WorkingDays",
                column: "ServiceProviderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingDays_ServiceProviders_ServiceProviderId1",
                table: "WorkingDays",
                column: "ServiceProviderId1",
                principalTable: "ServiceProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
