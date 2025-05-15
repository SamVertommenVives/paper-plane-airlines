using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PPA.Domains.Entities
{
    /// <inheritdoc />
    public partial class Update140526 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_Airport",
                table: "City");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Meal",
                type: "float",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SeatNumber",
                table: "FlightBooking",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Meal",
                table: "FlightBooking",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Airport",
                table: "City",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_City_Airport",
                table: "City",
                column: "Airport",
                principalTable: "Airport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_Airport",
                table: "City");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Meal");

            migrationBuilder.AlterColumn<string>(
                name: "SeatNumber",
                table: "FlightBooking",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Meal",
                table: "FlightBooking",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Airport",
                table: "City",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_City_Airport",
                table: "City",
                column: "Airport",
                principalTable: "Airport",
                principalColumn: "Id");
        }
    }
}
