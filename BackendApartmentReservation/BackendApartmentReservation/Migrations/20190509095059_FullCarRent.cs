using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class FullCarRent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Groups_DbGroupId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DbGroupId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DbGroupId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "ExternalGroupId",
                table: "Groups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AirportAddress",
                table: "FlightReservations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "FlightReservations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FlightTime",
                table: "FlightReservations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarAddress",
                table: "CarReservations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RentEndTime",
                table: "CarReservations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RentStartTime",
                table: "CarReservations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DbEmployeeGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DbEmployeeId = table.Column<int>(nullable: true),
                    DbGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbEmployeeGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbEmployeeGroup_Employees_DbEmployeeId",
                        column: x => x.DbEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DbEmployeeGroup_Groups_DbGroupId",
                        column: x => x.DbGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbEmployeeGroup_DbEmployeeId",
                table: "DbEmployeeGroup",
                column: "DbEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DbEmployeeGroup_DbGroupId",
                table: "DbEmployeeGroup",
                column: "DbGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbEmployeeGroup");

            migrationBuilder.DropColumn(
                name: "ExternalGroupId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "AirportAddress",
                table: "FlightReservations");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "FlightReservations");

            migrationBuilder.DropColumn(
                name: "FlightTime",
                table: "FlightReservations");

            migrationBuilder.DropColumn(
                name: "CarAddress",
                table: "CarReservations");

            migrationBuilder.DropColumn(
                name: "RentEndTime",
                table: "CarReservations");

            migrationBuilder.DropColumn(
                name: "RentStartTime",
                table: "CarReservations");

            migrationBuilder.AddColumn<int>(
                name: "DbGroupId",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DbGroupId",
                table: "Employees",
                column: "DbGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Groups_DbGroupId",
                table: "Employees",
                column: "DbGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
