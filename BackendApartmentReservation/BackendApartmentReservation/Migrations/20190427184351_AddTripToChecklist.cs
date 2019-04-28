using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class AddTripToChecklist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checklists_Groups_GroupId",
                table: "Checklists");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Checklists",
                newName: "TripId");

            migrationBuilder.RenameIndex(
                name: "IX_Checklists_GroupId",
                table: "Checklists",
                newName: "IX_Checklists_TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checklists_Trips_TripId",
                table: "Checklists",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checklists_Trips_TripId",
                table: "Checklists");

            migrationBuilder.RenameColumn(
                name: "TripId",
                table: "Checklists",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Checklists_TripId",
                table: "Checklists",
                newName: "IX_Checklists_GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checklists_Groups_GroupId",
                table: "Checklists",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
