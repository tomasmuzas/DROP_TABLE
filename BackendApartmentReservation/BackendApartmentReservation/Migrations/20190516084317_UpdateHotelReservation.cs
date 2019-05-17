using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class UpdateHotelReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "HotelReservations",
                newName: "HotelName");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFrom",
                table: "HotelReservations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTo",
                table: "HotelReservations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateFrom",
                table: "HotelReservations");

            migrationBuilder.DropColumn(
                name: "DateTo",
                table: "HotelReservations");

            migrationBuilder.RenameColumn(
                name: "HotelName",
                table: "HotelReservations",
                newName: "Address");
        }
    }
}
