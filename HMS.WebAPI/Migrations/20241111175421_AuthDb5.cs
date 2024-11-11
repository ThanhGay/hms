using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AuthDb5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuthRolePermission_PermissonKey",
                schema: "auth",
                table: "AuthRolePermission");

            migrationBuilder.CreateIndex(
                name: "IX_AuthRolePermission_PermissonKey",
                schema: "auth",
                table: "AuthRolePermission",
                column: "PermissonKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuthRolePermission_PermissonKey",
                schema: "auth",
                table: "AuthRolePermission");

            migrationBuilder.CreateIndex(
                name: "IX_AuthRolePermission_PermissonKey",
                schema: "auth",
                table: "AuthRolePermission",
                column: "PermissonKey",
                unique: true);
        }
    }
}
