using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.WebAPI.Migrations.HotelDb
{
    /// <inheritdoc />
    public partial class InitHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HolBillBooking_HolCharge_ChargeId",
                schema: "hol",
                table: "HolBillBooking");

            migrationBuilder.DropIndex(
                name: "IX_HolBillBooking_ChargeId",
                schema: "hol",
                table: "HolBillBooking");

            migrationBuilder.DropColumn(
                name: "ChargeId",
                schema: "hol",
                table: "HolBillBooking");

            migrationBuilder.DropColumn(
                name: "PercentDiscount",
                schema: "hol",
                table: "HolBillBooking");

            migrationBuilder.AlterColumn<int>(
                name: "ReceptionistID",
                schema: "hol",
                table: "HolBillBooking",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Prepayment",
                schema: "hol",
                table: "HolBillBooking",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountID",
                schema: "hol",
                table: "HolBillBooking",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                schema: "hol",
                table: "HolBillBooking",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOut",
                schema: "hol",
                table: "HolBillBooking",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckIn",
                schema: "hol",
                table: "HolBillBooking",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "HolBillBooking_Charge",
                schema: "hol",
                columns: table => new
                {
                    BillID = table.Column<int>(type: "int", nullable: false),
                    ChargeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolBillBooking_Charge", x => new { x.BillID, x.ChargeID });
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
                        principalColumn: "ChargeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HolBillBooking_Charge_BillID",
                schema: "hol",
                table: "HolBillBooking_Charge",
                column: "BillID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HolBillBooking_Charge_ChargeID",
                schema: "hol",
                table: "HolBillBooking_Charge",
                column: "ChargeID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HolBillBooking_Charge",
                schema: "hol");

            migrationBuilder.AlterColumn<int>(
                name: "ReceptionistID",
                schema: "hol",
                table: "HolBillBooking",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Prepayment",
                schema: "hol",
                table: "HolBillBooking",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DiscountID",
                schema: "hol",
                table: "HolBillBooking",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                schema: "hol",
                table: "HolBillBooking",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOut",
                schema: "hol",
                table: "HolBillBooking",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckIn",
                schema: "hol",
                table: "HolBillBooking",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChargeId",
                schema: "hol",
                table: "HolBillBooking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "PercentDiscount",
                schema: "hol",
                table: "HolBillBooking",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_HolBillBooking_ChargeId",
                schema: "hol",
                table: "HolBillBooking",
                column: "ChargeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HolBillBooking_HolCharge_ChargeId",
                schema: "hol",
                table: "HolBillBooking",
                column: "ChargeId",
                principalSchema: "hol",
                principalTable: "HolCharge",
                principalColumn: "ChargeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
