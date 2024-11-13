using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.WebAPI.Migrations.HotelDb
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "hol");

            migrationBuilder.CreateTable(
                name: "HolHotel",
                schema: "hol",
                columns: table => new
                {
                    HotelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    HotelAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Hotline = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolHotel", x => x.HotelID);
                });

            migrationBuilder.CreateTable(
                name: "HolRoomDetail",
                schema: "hol",
                columns: table => new
                {
                    RoomDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false)
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
                name: "HolPrice",
                schema: "hol",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PricePerHours = table.Column<int>(type: "int", nullable: false),
                    PricePerNight = table.Column<int>(type: "int", nullable: false),
                    PricePerHolidayHours = table.Column<int>(type: "int", nullable: false),
                    DayStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                        principalColumn: "HotelID",
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
                    table.PrimaryKey("PK_HolRoomType_RoomDetail", x => x.RoomDetailID);
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
                name: "IX_HolImage_RoomId",
                schema: "hol",
                table: "HolImage",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_HolPrice_RoomTypeID",
                schema: "hol",
                table: "HolPrice",
                column: "RoomTypeID");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HolImage",
                schema: "hol");

            migrationBuilder.DropTable(
                name: "HolPrice",
                schema: "hol");

            migrationBuilder.DropTable(
                name: "HolRoomType_RoomDetail",
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
