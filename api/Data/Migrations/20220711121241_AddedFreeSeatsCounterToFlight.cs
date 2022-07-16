using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Data.Migrations
{
    public partial class AddedFreeSeatsCounterToFlight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfSeats",
                table: "Flights",
                newName: "TotalSeats");

            migrationBuilder.AddColumn<int>(
                name: "RemainingSeats",
                table: "Flights",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingSeats",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "TotalSeats",
                table: "Flights",
                newName: "NumberOfSeats");
        }
    }
}
