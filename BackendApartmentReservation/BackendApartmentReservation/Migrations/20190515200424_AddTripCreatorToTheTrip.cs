using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class AddTripCreatorToTheTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TripCreatorId",
                table: "Trips",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TripCreatorId",
                table: "Trips",
                column: "TripCreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Employees_TripCreatorId",
                table: "Trips",
                column: "TripCreatorId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Employees_TripCreatorId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_TripCreatorId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TripCreatorId",
                table: "Trips");
        }
    }
}
