using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class AddExternalGroupId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.AddColumn<string>(
                name: "ExternalGroupId",
                table: "Groups",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalGroupId",
                table: "Groups");
        }
    }
}
