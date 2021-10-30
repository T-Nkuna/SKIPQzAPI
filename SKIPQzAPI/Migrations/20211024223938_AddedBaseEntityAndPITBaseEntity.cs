using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SKIPQzAPI.Migrations
{
    public partial class AddedBaseEntityAndPITBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Extras_Bookings_BookingId",
                table: "Extras");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceExtras_Extras_ExtraId",
                table: "ServiceExtras");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceExtras_Services_ServiceId",
                table: "ServiceExtras");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceProviderServices_Services_ServiceId",
                table: "ServiceProviderServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceProviderServices_ServiceProviders_ServiceProviderId",
                table: "ServiceProviderServices");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingDays_ServiceProviders_ServiceProviderId",
                table: "WorkingDays");

            migrationBuilder.DropIndex(
                name: "IX_WorkingDays_ServiceProviderId",
                table: "WorkingDays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceProviderServices",
                table: "ServiceProviderServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceProviders",
                table: "ServiceProviders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceExtras",
                table: "ServiceExtras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Extras",
                table: "Extras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientInfo",
                table: "ClientInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceProviderServicesId",
                table: "ServiceProviderServices");

            migrationBuilder.DropColumn(
                name: "ServiceProviderId",
                table: "ServiceProviders");

            migrationBuilder.DropColumn(
                name: "ServiceExtrasId",
                table: "ServiceExtras");

            migrationBuilder.DropColumn(
                name: "ExtraId",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "ClientInfoId",
                table: "ClientInfo");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Bookings");

            migrationBuilder.AddColumn<long>(
                name: "ServiceProviderId1",
                table: "WorkingDays",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Services",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Services",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Services",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Inactive",
                table: "Services",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                table: "Services",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Services",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrganisationId",
                table: "Services",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ServiceProviderId",
                table: "ServiceProviderServices",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ServiceId",
                table: "ServiceProviderServices",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "ServiceProviderServices",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "ServiceProviderServices",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ServiceProviderServices",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Inactive",
                table: "ServiceProviderServices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                table: "ServiceProviderServices",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "ServiceProviderServices",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrganisationId",
                table: "ServiceProviderServices",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "ServiceProviders",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "ServiceProviders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ServiceProviders",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Inactive",
                table: "ServiceProviders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                table: "ServiceProviders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "ServiceProviders",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrganisationId",
                table: "ServiceProviders",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ServiceId",
                table: "ServiceExtras",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ExtraId",
                table: "ServiceExtras",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "ServiceExtras",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "ServiceExtras",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ServiceExtras",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Inactive",
                table: "ServiceExtras",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                table: "ServiceExtras",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "ServiceExtras",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrganisationId",
                table: "ServiceExtras",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BookingId",
                table: "Extras",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Extras",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Extras",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Extras",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Inactive",
                table: "Extras",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                table: "Extras",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Extras",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrganisationId",
                table: "Extras",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "ClientInfo",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "ClientInfo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ClientInfo",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Inactive",
                table: "ClientInfo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                table: "ClientInfo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "ClientInfo",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrganisationId",
                table: "ClientInfo",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ServiceProviderId",
                table: "Bookings",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "ServiceId",
                table: "Bookings",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Bookings",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Bookings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Bookings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Inactive",
                table: "Bookings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "MasterId",
                table: "Bookings",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedBy",
                table: "Bookings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Bookings",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrganisationId",
                table: "Bookings",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceProviderServices",
                table: "ServiceProviderServices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceProviders",
                table: "ServiceProviders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceExtras",
                table: "ServiceExtras",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Extras",
                table: "Extras",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientInfo",
                table: "ClientInfo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDays_ServiceProviderId1",
                table: "WorkingDays",
                column: "ServiceProviderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Extras_Bookings_BookingId",
                table: "Extras",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceExtras_Extras_ExtraId",
                table: "ServiceExtras",
                column: "ExtraId",
                principalTable: "Extras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceExtras_Services_ServiceId",
                table: "ServiceExtras",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceProviderServices_Services_ServiceId",
                table: "ServiceProviderServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceProviderServices_ServiceProviders_ServiceProviderId",
                table: "ServiceProviderServices",
                column: "ServiceProviderId",
                principalTable: "ServiceProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingDays_ServiceProviders_ServiceProviderId1",
                table: "WorkingDays",
                column: "ServiceProviderId1",
                principalTable: "ServiceProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Extras_Bookings_BookingId",
                table: "Extras");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceExtras_Extras_ExtraId",
                table: "ServiceExtras");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceExtras_Services_ServiceId",
                table: "ServiceExtras");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceProviderServices_Services_ServiceId",
                table: "ServiceProviderServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceProviderServices_ServiceProviders_ServiceProviderId",
                table: "ServiceProviderServices");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingDays_ServiceProviders_ServiceProviderId1",
                table: "WorkingDays");

            migrationBuilder.DropIndex(
                name: "IX_WorkingDays_ServiceProviderId1",
                table: "WorkingDays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceProviderServices",
                table: "ServiceProviderServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceProviders",
                table: "ServiceProviders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceExtras",
                table: "ServiceExtras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Extras",
                table: "Extras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientInfo",
                table: "ClientInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ServiceProviderId1",
                table: "WorkingDays");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Inactive",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ServiceProviderServices");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ServiceProviderServices");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ServiceProviderServices");

            migrationBuilder.DropColumn(
                name: "Inactive",
                table: "ServiceProviderServices");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ServiceProviderServices");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "ServiceProviderServices");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "ServiceProviderServices");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ServiceProviders");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ServiceProviders");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ServiceProviders");

            migrationBuilder.DropColumn(
                name: "Inactive",
                table: "ServiceProviders");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ServiceProviders");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "ServiceProviders");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "ServiceProviders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ServiceExtras");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ServiceExtras");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ServiceExtras");

            migrationBuilder.DropColumn(
                name: "Inactive",
                table: "ServiceExtras");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ServiceExtras");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "ServiceExtras");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "ServiceExtras");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "Inactive",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ClientInfo");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ClientInfo");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ClientInfo");

            migrationBuilder.DropColumn(
                name: "Inactive",
                table: "ClientInfo");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ClientInfo");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "ClientInfo");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "ClientInfo");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Inactive",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "MasterId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceProviderId",
                table: "ServiceProviderServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "ServiceProviderServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceProviderServicesId",
                table: "ServiceProviderServices",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ServiceProviderId",
                table: "ServiceProviders",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "ServiceExtras",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExtraId",
                table: "ServiceExtras",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceExtrasId",
                table: "ServiceExtras",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "BookingId",
                table: "Extras",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExtraId",
                table: "Extras",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "ClientInfoId",
                table: "ClientInfo",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceProviderId",
                table: "Bookings",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "Bookings",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "ServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceProviderServices",
                table: "ServiceProviderServices",
                column: "ServiceProviderServicesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceProviders",
                table: "ServiceProviders",
                column: "ServiceProviderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceExtras",
                table: "ServiceExtras",
                column: "ServiceExtrasId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Extras",
                table: "Extras",
                column: "ExtraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientInfo",
                table: "ClientInfo",
                column: "ClientInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDays_ServiceProviderId",
                table: "WorkingDays",
                column: "ServiceProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Extras_Bookings_BookingId",
                table: "Extras",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceExtras_Extras_ExtraId",
                table: "ServiceExtras",
                column: "ExtraId",
                principalTable: "Extras",
                principalColumn: "ExtraId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceExtras_Services_ServiceId",
                table: "ServiceExtras",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceProviderServices_Services_ServiceId",
                table: "ServiceProviderServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceProviderServices_ServiceProviders_ServiceProviderId",
                table: "ServiceProviderServices",
                column: "ServiceProviderId",
                principalTable: "ServiceProviders",
                principalColumn: "ServiceProviderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingDays_ServiceProviders_ServiceProviderId",
                table: "WorkingDays",
                column: "ServiceProviderId",
                principalTable: "ServiceProviders",
                principalColumn: "ServiceProviderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
