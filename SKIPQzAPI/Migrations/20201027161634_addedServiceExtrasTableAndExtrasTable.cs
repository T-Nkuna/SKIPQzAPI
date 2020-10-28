using Microsoft.EntityFrameworkCore.Migrations;

namespace SKIPQzAPI.Migrations
{
    public partial class addedServiceExtrasTableAndExtrasTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Extras",
                columns: table => new
                {
                    ExtraId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extras", x => x.ExtraId);
                });

            migrationBuilder.CreateTable(
                name: "ServiceExtras",
                columns: table => new
                {
                    ServiceExtrasId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(nullable: true),
                    ExtraId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceExtras", x => x.ServiceExtrasId);
                    table.ForeignKey(
                        name: "FK_ServiceExtras_Extras_ExtraId",
                        column: x => x.ExtraId,
                        principalTable: "Extras",
                        principalColumn: "ExtraId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceExtras_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceExtras_ExtraId",
                table: "ServiceExtras",
                column: "ExtraId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceExtras_ServiceId",
                table: "ServiceExtras",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceExtras");

            migrationBuilder.DropTable(
                name: "Extras");
        }
    }
}
