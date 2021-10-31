using Microsoft.EntityFrameworkCore.Migrations;

namespace SKIPQzAPI.Migrations
{
    public partial class AddedUserToOrganisation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Organisations");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Organisations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_UserId",
                table: "Organisations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organisations_AspNetUsers_UserId",
                table: "Organisations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organisations_AspNetUsers_UserId",
                table: "Organisations");

            migrationBuilder.DropIndex(
                name: "IX_Organisations_UserId",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Organisations");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Organisations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Organisations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Organisations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
