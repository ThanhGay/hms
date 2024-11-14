using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.WebAPI.Migrations.BillBookingDb
{
    /// <inheritdoc />
    public partial class InitDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BillBillBookingRoom",
                schema: "bill",
                table: "BillBillBookingRoom");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillBillBookingRoom",
                schema: "bill",
                table: "BillBillBookingRoom",
                columns: new[] { "BillID", "RoomID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BillBillBookingRoom",
                schema: "bill",
                table: "BillBillBookingRoom");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillBillBookingRoom",
                schema: "bill",
                table: "BillBillBookingRoom",
                column: "BillID");
        }
    }
}
