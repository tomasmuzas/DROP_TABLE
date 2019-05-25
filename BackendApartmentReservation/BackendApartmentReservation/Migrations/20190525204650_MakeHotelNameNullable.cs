using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class MakeHotelNameNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HotelName",
                table: "HotelReservations",
                nullable: true,
                oldNullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HotelName",
                table: "HotelReservations",
                nullable: false,
                oldNullable: true);
        }
    }
}
