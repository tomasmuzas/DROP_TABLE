using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class AddExplicitForeignKeyForOfficeApartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Apartments_OfficeApartmentId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Offices_OfficeApartmentId",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "OfficeApartmentId",
                table: "Offices");

            migrationBuilder.AddColumn<int>(
                name: "DbApartmentId",
                table: "Offices",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Offices_DbApartmentId",
                table: "Offices",
                column: "DbApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Apartments_DbApartmentId",
                table: "Offices",
                column: "DbApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Apartments_DbApartmentId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Offices_DbApartmentId",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "DbApartmentId",
                table: "Offices");

            migrationBuilder.AddColumn<int>(
                name: "OfficeApartmentId",
                table: "Offices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offices_OfficeApartmentId",
                table: "Offices",
                column: "OfficeApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Apartments_OfficeApartmentId",
                table: "Offices",
                column: "OfficeApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
