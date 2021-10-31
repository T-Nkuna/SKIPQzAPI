using Microsoft.EntityFrameworkCore.Migrations;

namespace SKIPQzAPI.Migrations
{
    public partial class pendingMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Organisations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Organisations");
        }
    }
}
