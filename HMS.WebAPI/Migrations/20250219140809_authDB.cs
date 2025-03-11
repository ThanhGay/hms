using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class authDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.CreateTable(
                name: "AuthFavouriteRoom",
                schema: "auth",
                columns: table => new
                {
                    FavouriteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthFavouriteRoom", x => x.FavouriteId);
                    table.ForeignKey(
                        name: "FK_AuthFavouriteRoom_AuthCustomer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "auth",
                        principalTable: "AuthCustomer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthCustomerVoucher",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AuthFavouriteRoom",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AuthReceptionist",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AuthRolePermission",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AuthVoucher",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AuthCustomer",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AuthPermission",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AuthUser",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AuthRole",
                schema: "auth");
        }
    }
}
