using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class RemoveTripCostAndAddExternalId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Trips");

            migrationBuilder.AddColumn<string>(
                name: "ExternalTripId",
                table: "Trips",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalTripId",
                table: "Trips");

            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "Trips",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
