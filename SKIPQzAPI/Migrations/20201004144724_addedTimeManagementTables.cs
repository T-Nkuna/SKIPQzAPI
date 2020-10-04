using Microsoft.EntityFrameworkCore.Migrations;

namespace SKIPQzAPI.Migrations
{
    public partial class addedTimeManagementTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceProviders_Schedules_ScheduleId",
                table: "ServiceProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Schedules_ScheduleId",
                table: "Services");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Services_ScheduleId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_ServiceProviders_ScheduleId",
                table: "ServiceProviders");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "ServiceProviders");

            migrationBuilder.CreateTable(
                name: "TimeComponents",
                columns: table => new
                {
                    TimeComponentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hour = table.Column<double>(nullable: false),
                    Minute = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeComponents", x => x.TimeComponentId);
                });

            migrationBuilder.CreateTable(
                name: "WorkingDay",
                columns: table => new
                {
                    WorkingDayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekDay = table.Column<int>(nullable: false),
                    ServiceProviderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingDay", x => x.WorkingDayId);
                    table.ForeignKey(
                        name: "FK_WorkingDay_ServiceProviders_ServiceProviderId",
                        column: x => x.ServiceProviderId,
                        principalTable: "ServiceProviders",
                        principalColumn: "ServiceProviderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeComponentIntervals",
                columns: table => new
                {
                    TimeComponentIntervalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTimeTimeComponentId = table.Column<int>(nullable: true),
                    EndTimeTimeComponentId = table.Column<int>(nullable: true),
                    WorkingDayId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeComponentIntervals", x => x.TimeComponentIntervalId);
                    table.ForeignKey(
                        name: "FK_TimeComponentIntervals_TimeComponents_EndTimeTimeComponentId",
                        column: x => x.EndTimeTimeComponentId,
                        principalTable: "TimeComponents",
                        principalColumn: "TimeComponentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeComponentIntervals_TimeComponents_StartTimeTimeComponentId",
                        column: x => x.StartTimeTimeComponentId,
                        principalTable: "TimeComponents",
                        principalColumn: "TimeComponentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeComponentIntervals_WorkingDay_WorkingDayId",
                        column: x => x.WorkingDayId,
                        principalTable: "WorkingDay",
                        principalColumn: "WorkingDayId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeComponentIntervals_EndTimeTimeComponentId",
                table: "TimeComponentIntervals",
                column: "EndTimeTimeComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeComponentIntervals_StartTimeTimeComponentId",
                table: "TimeComponentIntervals",
                column: "StartTimeTimeComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeComponentIntervals_WorkingDayId",
                table: "TimeComponentIntervals",
                column: "WorkingDayId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDay_ServiceProviderId",
                table: "WorkingDay",
                column: "ServiceProviderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeComponentIntervals");

            migrationBuilder.DropTable(
                name: "TimeComponents");

            migrationBuilder.DropTable(
                name: "WorkingDay");

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "ServiceProviders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Owner = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    TimeSlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<int>(type: "int", nullable: true),
                    TheDayOfWeek = table.Column<int>(type: "int", nullable: false),
                    TheTimeOfDay = table.Column<int>(type: "int", nullable: false),
                    TimeSlotString = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.TimeSlotId);
                    table.ForeignKey(
                        name: "FK_TimeSlots_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_ScheduleId",
                table: "Services",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviders_ScheduleId",
                table: "ServiceProviders",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_ScheduleId",
                table: "TimeSlots",
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
    }
}
