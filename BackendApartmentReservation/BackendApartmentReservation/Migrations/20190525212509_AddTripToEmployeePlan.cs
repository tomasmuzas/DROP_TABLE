using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class AddTripToEmployeePlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssociatedTripId",
                table: "EmployeePlans",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePlans_AssociatedTripId",
                table: "EmployeePlans",
                column: "AssociatedTripId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePlans_Trips_AssociatedTripId",
                table: "EmployeePlans",
                column: "AssociatedTripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePlans_Trips_AssociatedTripId",
                table: "EmployeePlans");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePlans_AssociatedTripId",
                table: "EmployeePlans");

            migrationBuilder.DropColumn(
                name: "AssociatedTripId",
                table: "EmployeePlans");
        }
    }
}
