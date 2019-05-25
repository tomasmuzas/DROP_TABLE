using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class AddFileToFlightInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "FlightAmenities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlightAmenities_TicketId",
                table: "FlightAmenities",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightAmenities_Files_TicketId",
                table: "FlightAmenities",
                column: "TicketId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightAmenities_Files_TicketId",
                table: "FlightAmenities");

            migrationBuilder.DropIndex(
                name: "IX_FlightAmenities_TicketId",
                table: "FlightAmenities");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "FlightAmenities");
        }
    }
}
