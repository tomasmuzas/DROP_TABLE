using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class AddDocumentsFileToCarRentInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentsId",
                table: "CarRentAmenities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarRentAmenities_DocumentsId",
                table: "CarRentAmenities",
                column: "DocumentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentAmenities_Files_DocumentsId",
                table: "CarRentAmenities",
                column: "DocumentsId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRentAmenities_Files_DocumentsId",
                table: "CarRentAmenities");

            migrationBuilder.DropIndex(
                name: "IX_CarRentAmenities_DocumentsId",
                table: "CarRentAmenities");

            migrationBuilder.DropColumn(
                name: "DocumentsId",
                table: "CarRentAmenities");
        }
    }
}
