using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.WebAPI.Migrations.HotelDb
{
    /// <inheritdoc />
    public partial class Db1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HolCharge_HolBillBooking_ChargeId",
                schema: "hol",
                table: "HolCharge");

            migrationBuilder.AlterColumn<int>(
                name: "ChargeId",
                schema: "hol",
                table: "HolCharge",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_HolBillBooking_ChargeId",
                schema: "hol",
                table: "HolBillBooking",
                column: "ChargeId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HolBillBooking_HolCharge_ChargeId",
                schema: "hol",
                table: "HolBillBooking");

            migrationBuilder.DropIndex(
                name: "IX_HolBillBooking_ChargeId",
                schema: "hol",
                table: "HolBillBooking");

            migrationBuilder.AlterColumn<int>(
                name: "ChargeId",
                schema: "hol",
                table: "HolCharge",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_HolCharge_HolBillBooking_ChargeId",
                schema: "hol",
                table: "HolCharge",
                column: "ChargeId",
                principalSchema: "hol",
                principalTable: "HolBillBooking",
                principalColumn: "BillID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
