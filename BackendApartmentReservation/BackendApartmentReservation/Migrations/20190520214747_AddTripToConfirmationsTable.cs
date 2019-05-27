using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class AddTripToConfirmationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "Confirmations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Confirmations_TripId",
                table: "Confirmations",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Confirmations_Trips_TripId",
                table: "Confirmations",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Confirmations_Trips_TripId",
                table: "Confirmations");

            migrationBuilder.DropIndex(
                name: "IX_Confirmations_TripId",
                table: "Confirmations");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Confirmations");
        }
    }
}
