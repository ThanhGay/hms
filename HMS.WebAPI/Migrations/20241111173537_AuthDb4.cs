using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AuthDb4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthManager",
                schema: "auth");

            migrationBuilder.CreateTable(
                name: "AuthPermission",
                schema: "auth",
                columns: table => new
                {
                    PermissonKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PermissionName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthPermission", x => x.PermissonKey);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthRolePermission_PermissonKey",
                schema: "auth",
                table: "AuthRolePermission",
                column: "PermissonKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthRolePermission_AuthPermission_PermissonKey",
                schema: "auth",
                table: "AuthRolePermission",
                column: "PermissonKey",
                principalSchema: "auth",
                principalTable: "AuthPermission",
                principalColumn: "PermissonKey",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthRolePermission_AuthPermission_PermissonKey",
                schema: "auth",
                table: "AuthRolePermission");

            migrationBuilder.DropTable(
                name: "AuthPermission",
                schema: "auth");

            migrationBuilder.DropIndex(
                name: "IX_AuthRolePermission_PermissonKey",
                schema: "auth",
                table: "AuthRolePermission");

            migrationBuilder.CreateTable(
                name: "AuthManager",
                schema: "auth",
                columns: table => new
                {
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    CitizenIdentity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthManager", x => x.ManagerId);
                    table.ForeignKey(
                        name: "FK_AuthManager_AuthUser_ManagerId",
                        column: x => x.ManagerId,
                        principalSchema: "auth",
                        principalTable: "AuthUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });
        }
    }
}
