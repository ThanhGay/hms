using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.WebAPI.Migrations.HotelDb
{
    /// <inheritdoc />
    public partial class InitDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HolRoomType_RoomDetail",
                schema: "hol",
                table: "HolRoomType_RoomDetail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HolRoomType_RoomDetail",
                schema: "hol",
                table: "HolRoomType_RoomDetail",
                columns: new[] { "RoomDetailID", "RoomTypeID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HolRoomType_RoomDetail",
                schema: "hol",
                table: "HolRoomType_RoomDetail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HolRoomType_RoomDetail",
                schema: "hol",
                table: "HolRoomType_RoomDetail",
                column: "RoomDetailID");
        }
    }
}
