using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitDb4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthVoucher",
                schema: "auth",
                columns: table => new
                {
                    VoucherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Percent = table.Column<float>(type: "real", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthVoucher", x => x.VoucherId);
                });

            migrationBuilder.CreateTable(
                name: "AuthCustomerVoucher",
                schema: "auth",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    VoucherId = table.Column<int>(type: "int", nullable: false),
                    UsedAt = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthCustomerVoucher", x => new { x.VoucherId, x.CustomerId });
                    table.ForeignKey(
                        name: "FK_AuthCustomerVoucher_AuthCustomer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "auth",
                        principalTable: "AuthCustomer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthCustomerVoucher_AuthVoucher_VoucherId",
                        column: x => x.VoucherId,
                        principalSchema: "auth",
                        principalTable: "AuthVoucher",
                        principalColumn: "VoucherId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthCustomerVoucher_CustomerId",
                schema: "auth",
                table: "AuthCustomerVoucher",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthCustomerVoucher",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AuthVoucher",
                schema: "auth");
        }
    }
}
