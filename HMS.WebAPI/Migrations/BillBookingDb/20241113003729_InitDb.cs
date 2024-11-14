using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.WebAPI.Migrations.BillBookingDb
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bill");

            migrationBuilder.CreateTable(
                name: "BillDiscount",
                schema: "bill",
                columns: table => new
                {
                    DiscountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Percent = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<int>(type: "int", nullable: false),
                    ExpDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDiscount", x => x.DiscountID);
                });

            migrationBuilder.CreateTable(
                name: "BillPayment",
                schema: "bill",
                columns: table => new
                {
                    BillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillPayment", x => x.BillID);
                });

            migrationBuilder.CreateTable(
                name: "BillBillBooking",
                schema: "bill",
                columns: table => new
                {
                    BillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrePayment = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    ReceptionistID = table.Column<int>(type: "int", nullable: false),
                    Charge = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillBillBooking", x => x.BillID);
                    table.ForeignKey(
                        name: "FK_BillBillBooking_BillDiscount_DiscountID",
                        column: x => x.DiscountID,
                        principalSchema: "bill",
                        principalTable: "BillDiscount",
                        principalColumn: "DiscountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillBillBookingRoom",
                schema: "bill",
                columns: table => new
                {
                    BillID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillBillBookingRoom", x => x.BillID);
                    table.ForeignKey(
                        name: "FK_BillBillBookingRoom_BillBillBooking_BillID",
                        column: x => x.BillID,
                        principalSchema: "bill",
                        principalTable: "BillBillBooking",
                        principalColumn: "BillID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillBillBooking_DiscountID",
                schema: "bill",
                table: "BillBillBooking",
                column: "DiscountID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillBillBookingRoom",
                schema: "bill");

            migrationBuilder.DropTable(
                name: "BillPayment",
                schema: "bill");

            migrationBuilder.DropTable(
                name: "BillBillBooking",
                schema: "bill");

            migrationBuilder.DropTable(
                name: "BillDiscount",
                schema: "bill");
        }
    }
}
