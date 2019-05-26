using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class AddIndexesOnExternalIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "ExternalTripId",
                "Trips",
                oldMaxLength: null,
                maxLength: 50);

            migrationBuilder.CreateIndex(
                "IX_Trips_ExternalTripId",
                "Trips",
                "ExternalTripId");

            migrationBuilder.AlterColumn<string>(
                "ExternalEmployeeId",
                "Employees",
                oldMaxLength: null,
                maxLength: 50);

            migrationBuilder.CreateIndex(
                "IX_Employees_ExternalEmployeeId",
                "Employees",
                "ExternalEmployeeId");

            migrationBuilder.AlterColumn<string>(
                "ExternalConfirmationId",
                "Confirmations",
                oldMaxLength: null,
                maxLength: 50);

            migrationBuilder.CreateIndex(
                "IX_Confirmations_ExternalConfirmationId",
                "Confirmations",
                "ExternalConfirmationId");

            migrationBuilder.AlterColumn<string>(
                "ExternalOfficeId",
                "Offices",
                oldMaxLength: null,
                maxLength: 50);

            migrationBuilder.CreateIndex(
                "IX_Offices_ExternalOfficeId",
                "Offices",
                "ExternalOfficeId");

            migrationBuilder.AlterColumn<string>(
                "ExternalFileId",
                "Files",
                oldMaxLength: null,
                maxLength: 50);

            migrationBuilder.CreateIndex(
                "IX_Files_ExternalFileId",
                "Files",
                "ExternalFileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                "IX_Employees_ExternalEmployeeId",
                "Employees");

            migrationBuilder.DropIndex(
                "IX_Confirmations_ExternalConfirmationId",
                "Confirmations");

            migrationBuilder.DropIndex(
                "IX_Offices_ExternalOfficeId",
                "Offices");

            migrationBuilder.DropIndex(
                "IX_Trips_ExternalTripId",
                "Trips");

            migrationBuilder.DropIndex(
                "IX_Files_ExternalFileId",
                "Files");

            migrationBuilder.AlterColumn<string>(
                "ExternalOfficeId",
                "Offices",
                oldMaxLength: 50,
                maxLength: null);

            migrationBuilder.AlterColumn<string>(
                "ExternalTripId",
                "Trips",
                oldMaxLength: 50,
                maxLength: null);

            migrationBuilder.AlterColumn<string>(
                "ExternalConfirmationId",
                "Confirmations",
                oldMaxLength: 50,
                maxLength: null);

            migrationBuilder.AlterColumn<string>(
                "ExternalEmployeeId",
                "Employees",
                oldMaxLength: 50,
                maxLength: null);

            migrationBuilder.AlterColumn<string>(
                "ExternalFileId",
                "Files",
                oldMaxLength: 50,
                maxLength: null);
        }
    }
}
