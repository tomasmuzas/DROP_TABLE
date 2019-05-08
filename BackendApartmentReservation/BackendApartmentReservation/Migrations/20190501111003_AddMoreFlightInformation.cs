using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class AddMoreFlightInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirportAddress",
                table: "FlightReservations");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "FlightReservations");

            migrationBuilder.DropColumn(
                name: "FlightTime",
                table: "FlightReservations");
        }
    }
}
