using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class AddFileToHotelReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentsId",
                table: "HotelReservations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotelReservations_DocumentsId",
                table: "HotelReservations",
                column: "DocumentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_Files_DocumentsId",
                table: "HotelReservations",
                column: "DocumentsId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Files_DocumentsId",
                table: "HotelReservations");

            migrationBuilder.DropIndex(
                name: "IX_HotelReservations_DocumentsId",
                table: "HotelReservations");

            migrationBuilder.DropColumn(
                name: "DocumentsId",
                table: "HotelReservations");
        }
    }
}
