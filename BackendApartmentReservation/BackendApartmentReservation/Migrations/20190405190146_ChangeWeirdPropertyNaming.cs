using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class ChangeWeirdPropertyNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRentAmenities_CarReservations_CarId",
                table: "CarRentAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightAmenities_FlightReservations_ReservationId",
                table: "FlightAmenities");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "FlightAmenities",
                newName: "FlightReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightAmenities_ReservationId",
                table: "FlightAmenities",
                newName: "IX_FlightAmenities_FlightReservationId");

            migrationBuilder.RenameColumn(
                name: "CardPlateNumber",
                table: "CarReservations",
                newName: "CarNumber");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "CarRentAmenities",
                newName: "CarReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_CarRentAmenities_CarId",
                table: "CarRentAmenities",
                newName: "IX_CarRentAmenities_CarReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentAmenities_CarReservations_CarReservationId",
                table: "CarRentAmenities",
                column: "CarReservationId",
                principalTable: "CarReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightAmenities_FlightReservations_FlightReservationId",
                table: "FlightAmenities",
                column: "FlightReservationId",
                principalTable: "FlightReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRentAmenities_CarReservations_CarReservationId",
                table: "CarRentAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightAmenities_FlightReservations_FlightReservationId",
                table: "FlightAmenities");

            migrationBuilder.RenameColumn(
                name: "FlightReservationId",
                table: "FlightAmenities",
                newName: "ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightAmenities_FlightReservationId",
                table: "FlightAmenities",
                newName: "IX_FlightAmenities_ReservationId");

            migrationBuilder.RenameColumn(
                name: "CarNumber",
                table: "CarReservations",
                newName: "CardPlateNumber");

            migrationBuilder.RenameColumn(
                name: "CarReservationId",
                table: "CarRentAmenities",
                newName: "CarId");

            migrationBuilder.RenameIndex(
                name: "IX_CarRentAmenities_CarReservationId",
                table: "CarRentAmenities",
                newName: "IX_CarRentAmenities_CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentAmenities_CarReservations_CarId",
                table: "CarRentAmenities",
                column: "CarId",
                principalTable: "CarReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightAmenities_FlightReservations_ReservationId",
                table: "FlightAmenities",
                column: "ReservationId",
                principalTable: "FlightReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
