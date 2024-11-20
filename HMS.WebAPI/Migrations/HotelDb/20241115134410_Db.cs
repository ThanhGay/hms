using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.WebAPI.Migrations.HotelDb
{
    /// <inheritdoc />
    public partial class Db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HolPrice",
                schema: "hol");

            migrationBuilder.CreateTable(
                name: "HolBillBooking",
                schema: "hol",
                columns: table => new
                {
                    BillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Prepayment = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountID = table.Column<int>(type: "int", nullable: false),
                    ChargeId = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    ReceptionistID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolBillBooking", x => x.BillID);
                });

            migrationBuilder.CreateTable(
                name: "HolDefaultPrice",
                schema: "hol",
                columns: table => new
                {
                    DefaultPriceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PricePerHour = table.Column<int>(type: "int", nullable: false),
                    PricePerNight = table.Column<int>(type: "int", nullable: false),
                    RoomTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolDefaultPrice", x => x.DefaultPriceID);
                    table.ForeignKey(
                        name: "FK_HolDefaultPrice_HolRoomType_RoomTypeID",
                        column: x => x.RoomTypeID,
                        principalSchema: "hol",
                        principalTable: "HolRoomType",
                        principalColumn: "RoomTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HolSubPrice",
                schema: "hol",
                columns: table => new
                {
                    SubPriceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PricePerHours = table.Column<int>(type: "int", nullable: false),
                    PricePerNight = table.Column<int>(type: "int", nullable: false),
                    DayStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoomTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolSubPrice", x => x.SubPriceID);
                    table.ForeignKey(
                        name: "FK_HolSubPrice_HolRoomType_RoomTypeID",
                        column: x => x.RoomTypeID,
                        principalSchema: "hol",
                        principalTable: "HolRoomType",
                        principalColumn: "RoomTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HolBillBooking_Room",
                schema: "hol",
                columns: table => new
                {
                    BillID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolBillBooking_Room", x => new { x.BillID, x.RoomID });
                    table.ForeignKey(
                        name: "FK_HolBillBooking_Room_HolBillBooking_BillID",
                        column: x => x.BillID,
                        principalSchema: "hol",
                        principalTable: "HolBillBooking",
                        principalColumn: "BillID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HolBillBooking_Room_HolRoom_RoomID",
                        column: x => x.RoomID,
                        principalSchema: "hol",
                        principalTable: "HolRoom",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HolCharge",
                schema: "hol",
                columns: table => new
                {
                    ChargeId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Descreption = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolCharge", x => x.ChargeId);
                    table.ForeignKey(
                        name: "FK_HolCharge_HolBillBooking_ChargeId",
                        column: x => x.ChargeId,
                        principalSchema: "hol",
                        principalTable: "HolBillBooking",
                        principalColumn: "BillID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HolDiscount",
                schema: "hol",
                columns: table => new
                {
                    DiscountID = table.Column<int>(type: "int", nullable: false),
                    Percent = table.Column<float>(type: "real", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolDiscount", x => x.DiscountID);
                    table.ForeignKey(
                        name: "FK_HolDiscount_HolBillBooking_DiscountID",
                        column: x => x.DiscountID,
                        principalSchema: "hol",
                        principalTable: "HolBillBooking",
                        principalColumn: "BillID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HolBillBooking_Room_RoomID",
                schema: "hol",
                table: "HolBillBooking_Room",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_HolDefaultPrice_RoomTypeID",
                schema: "hol",
                table: "HolDefaultPrice",
                column: "RoomTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_HolSubPrice_RoomTypeID",
                schema: "hol",
                table: "HolSubPrice",
                column: "RoomTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HolBillBooking_Room",
                schema: "hol");

            migrationBuilder.DropTable(
                name: "HolCharge",
                schema: "hol");

            migrationBuilder.DropTable(
                name: "HolDefaultPrice",
                schema: "hol");

            migrationBuilder.DropTable(
                name: "HolDiscount",
                schema: "hol");

            migrationBuilder.DropTable(
                name: "HolSubPrice",
                schema: "hol");

            migrationBuilder.DropTable(
                name: "HolBillBooking",
                schema: "hol");

            migrationBuilder.CreateTable(
                name: "HolPrice",
                schema: "hol",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PricePerHolidayHours = table.Column<int>(type: "int", nullable: false),
                    PricePerHours = table.Column<int>(type: "int", nullable: false),
                    PricePerNight = table.Column<int>(type: "int", nullable: false),
                    RoomTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolPrice", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_HolPrice_HolRoomType_RoomTypeID",
                        column: x => x.RoomTypeID,
                        principalSchema: "hol",
                        principalTable: "HolRoomType",
                        principalColumn: "RoomTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HolPrice_RoomTypeID",
                schema: "hol",
                table: "HolPrice",
                column: "RoomTypeID");
        }
    }
}
