using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "auth");

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

            migrationBuilder.CreateTable(
                name: "AuthRole",
                schema: "auth",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthRole", x => x.RoleId);
                });

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
                name: "AuthRolePermission",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissonKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthRolePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthRolePermission_AuthPermission_PermissonKey",
                        column: x => x.PermissonKey,
                        principalSchema: "auth",
                        principalTable: "AuthPermission",
                        principalColumn: "PermissonKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthRolePermission_AuthRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "auth",
                        principalTable: "AuthRole",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuthUser",
                schema: "auth",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUser", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_AuthUser_AuthRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "auth",
                        principalTable: "AuthRole",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuthCustomer",
                schema: "auth",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitizenIdentity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthCustomer", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_AuthCustomer_AuthUser_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "auth",
                        principalTable: "AuthUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuthReceptionist",
                schema: "auth",
                columns: table => new
                {
                    ReceptionistId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitizenIdentity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthReceptionist", x => x.ReceptionistId);
                    table.ForeignKey(
                        name: "FK_AuthReceptionist_AuthUser_ReceptionistId",
                        column: x => x.ReceptionistId,
                        principalSchema: "auth",
                        principalTable: "AuthUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateIndex(
                name: "IX_AuthRolePermission_PermissonKey",
                schema: "auth",
                table: "AuthRolePermission",
                column: "PermissonKey");

            migrationBuilder.CreateIndex(
                name: "IX_AuthRolePermission_RoleId",
                schema: "auth",
                table: "AuthRolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthUser_RoleId",
                schema: "auth",
                table: "AuthUser",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthCustomerVoucher",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AuthReceptionist",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AuthRolePermission",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AuthCustomer",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AuthVoucher",
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
