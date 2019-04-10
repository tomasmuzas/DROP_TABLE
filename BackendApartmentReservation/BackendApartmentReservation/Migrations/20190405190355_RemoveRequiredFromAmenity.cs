using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class RemoveRequiredFromAmenity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Required",
                table: "LivingPlaceAmenities");

            migrationBuilder.DropColumn(
                name: "Required",
                table: "FlightAmenities");

            migrationBuilder.DropColumn(
                name: "Required",
                table: "CarRentAmenities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Required",
                table: "LivingPlaceAmenities",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Required",
                table: "FlightAmenities",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Required",
                table: "CarRentAmenities",
                nullable: false,
                defaultValue: false);
        }
    }
}
