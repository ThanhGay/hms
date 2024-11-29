using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.WebAPI.Migrations.HotelDb
{
    /// <inheritdoc />
    public partial class updateB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "hol");

            migrationBuilder.CreateTable(
                name: "HolBillBooking",
                schema: "hol",
                columns: table => new
                {
                    BillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectedCheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectedCheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Prepayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountID = table.Column<int>(type: "int", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    ReceptionistID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolBillBooking", x => x.BillID);
                });

            migrationBuilder.CreateTable(
                name: "HolCharge",
                schema: "hol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descreption = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolCharge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HolHotel",
                schema: "hol",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    HotelAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Hotline = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolHotel", x => x.HotelId);
                });

            migrationBuilder.CreateTable(
                name: "HolRoomDetail",
                schema: "hol",
                columns: table => new
                {
                    RoomDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolRoomDetail", x => x.RoomDetailID);
                });

            migrationBuilder.CreateTable(
                name: "HolRoomType",
                schema: "hol",
                columns: table => new
                {
                    RoomTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolRoomType", x => x.RoomTypeID);
                });

            migrationBuilder.CreateTable(
                name: "HolBillBooking_Charge",
                schema: "hol",
                columns: table => new
                {
                    Booking_ChargeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillID = table.Column<int>(type: "int", nullable: false),
                    ChargeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolBillBooking_Charge", x => x.Booking_ChargeID);
                    table.ForeignKey(
                        name: "FK_HolBillBooking_Charge_HolBillBooking_BillID",
                        column: x => x.BillID,
                        principalSchema: "hol",
                        principalTable: "HolBillBooking",
                        principalColumn: "BillID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HolBillBooking_Charge_HolCharge_ChargeID",
                        column: x => x.ChargeID,
                        principalSchema: "hol",
                        principalTable: "HolCharge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HolDefaultPrice",
                schema: "hol",
                columns: table => new
                {
                    DefaultPriceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                name: "HolRoom",
                schema: "hol",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    RoomTypeId = table.Column<int>(type: "int", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolRoom", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_HolRoom_HolHotel_HotelId",
                        column: x => x.HotelId,
                        principalSchema: "hol",
                        principalTable: "HolHotel",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HolRoom_HolRoomType_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalSchema: "hol",
                        principalTable: "HolRoomType",
                        principalColumn: "RoomTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HolRoomType_RoomDetail",
                schema: "hol",
                columns: table => new
                {
                    RoomDetailID = table.Column<int>(type: "int", nullable: false),
                    RoomTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolRoomType_RoomDetail", x => new { x.RoomDetailID, x.RoomTypeID });
                    table.ForeignKey(
                        name: "FK_HolRoomType_RoomDetail_HolRoomDetail_RoomDetailID",
                        column: x => x.RoomDetailID,
                        principalSchema: "hol",
                        principalTable: "HolRoomDetail",
                        principalColumn: "RoomDetailID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HolRoomType_RoomDetail_HolRoomType_RoomTypeID",
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
                    PricePerHours = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolBillBooking_Room", x => x.ID);
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
                name: "HolImage",
                schema: "hol",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolImage", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_HolImage_HolRoom_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "hol",
                        principalTable: "HolRoom",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HolBillBooking_Charge_BillID",
                schema: "hol",
                table: "HolBillBooking_Charge",
                column: "BillID");

            migrationBuilder.CreateIndex(
                name: "IX_HolBillBooking_Charge_ChargeID",
                schema: "hol",
                table: "HolBillBooking_Charge",
                column: "ChargeID");

            migrationBuilder.CreateIndex(
                name: "IX_HolBillBooking_Room_BillID",
                schema: "hol",
                table: "HolBillBooking_Room",
                column: "BillID");

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
                name: "IX_HolImage_RoomId",
                schema: "hol",
                table: "HolImage",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_HolRoom_HotelId",
                schema: "hol",
                table: "HolRoom",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_HolRoom_RoomTypeId",
                schema: "hol",
                table: "HolRoom",
                column: "RoomTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HolRoomType_RoomDetail_RoomTypeID",
                schema: "hol",
                table: "HolRoomType_RoomDetail",
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
                name: "HolBillBooking_Charge",
                schema: "hol");

            migrationBuilder.DropTable(
                name: "HolBillBooking_Room",
                schema: "hol");

            migrationBuilder.DropTable(
                name: "HolDefaultPrice",
                schema: "hol");

            migrationBuilder.DropTable(
                name: "HolImage",
                schema: "hol");

            migrationBuilder.DropTable(
                name: "HolRoomType_RoomDetail",
                schema: "hol");

            migrationBuilder.DropTable(
                name: "HolSubPrice",
                schema: "hol");

            migrationBuilder.DropTable(
                name: "HolCharge",
                schema: "hol");

            migrationBuilder.DropTable(
                name: "HolBillBooking",
                schema: "hol");

            migrationBuilder.DropTable(
                name: "HolRoom",
                schema: "hol");

            migrationBuilder.DropTable(
                name: "HolRoomDetail",
                schema: "hol");

            migrationBuilder.DropTable(
                name: "HolHotel",
                schema: "hol");

            migrationBuilder.DropTable(
                name: "HolRoomType",
                schema: "hol");
        }
    }
}
