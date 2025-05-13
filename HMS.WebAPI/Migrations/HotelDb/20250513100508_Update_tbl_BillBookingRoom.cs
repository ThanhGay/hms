using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.WebAPI.Migrations.HotelDb
{
    /// <inheritdoc />
    public partial class Update_tbl_BillBookingRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PricePerHour",
                schema: "hol",
                table: "HolBillBooking_Room",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerNight",
                schema: "hol",
                table: "HolBillBooking_Room",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "RoomTypeDescription",
                schema: "hol",
                table: "HolBillBooking_Room",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoomTypeName",
                schema: "hol",
                table: "HolBillBooking_Room",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_HolRoomReview",
                schema: "hol",
                table: "HolRoomReview",
                columns: new[] { "Id", "Star", "IsDeleted" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HolRoomReview",
                schema: "hol",
                table: "HolRoomReview");

            migrationBuilder.DropColumn(
                name: "PricePerHour",
                schema: "hol",
                table: "HolBillBooking_Room");

            migrationBuilder.DropColumn(
                name: "PricePerNight",
                schema: "hol",
                table: "HolBillBooking_Room");

            migrationBuilder.DropColumn(
                name: "RoomTypeDescription",
                schema: "hol",
                table: "HolBillBooking_Room");

            migrationBuilder.DropColumn(
                name: "RoomTypeName",
                schema: "hol",
                table: "HolBillBooking_Room");
        }
    }
}
