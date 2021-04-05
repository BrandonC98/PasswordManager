using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PasswordManagerData.Migrations
{
    public partial class RefactorModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterPasswords_users_UserId",
                table: "MasterPasswords");

            migrationBuilder.DropForeignKey(
                name: "FK_websites_users_UserId",
                table: "websites");

            migrationBuilder.DropTable(
                name: "AccessHistories");

            migrationBuilder.DropTable(
                name: "Blacklists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_websites",
                table: "websites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.RenameTable(
                name: "websites",
                newName: "Websites");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_websites_UserId",
                table: "Websites",
                newName: "IX_Websites_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Websites",
                table: "Websites",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPasswords_Users_UserId",
                table: "MasterPasswords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Websites_Users_UserId",
                table: "Websites",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterPasswords_Users_UserId",
                table: "MasterPasswords");

            migrationBuilder.DropForeignKey(
                name: "FK_Websites_Users_UserId",
                table: "Websites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Websites",
                table: "Websites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Websites",
                newName: "websites");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameIndex(
                name: "IX_Websites_UserId",
                table: "websites",
                newName: "IX_websites_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_websites",
                table: "websites",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AccessHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessHistories_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blacklists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blacklists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blacklists_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessHistories_UserId",
                table: "AccessHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blacklists_UserId",
                table: "Blacklists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPasswords_users_UserId",
                table: "MasterPasswords",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_websites_users_UserId",
                table: "websites",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
