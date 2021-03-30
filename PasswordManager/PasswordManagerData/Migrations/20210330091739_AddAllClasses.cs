using Microsoft.EntityFrameworkCore.Migrations;

namespace PasswordManagerData.Migrations
{
    public partial class AddAllClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessHistories_users_UserId",
                table: "AccessHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Blacklists_users_UserId",
                table: "Blacklists");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Blacklists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AccessHistories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessHistories_users_UserId",
                table: "AccessHistories",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Blacklists_users_UserId",
                table: "Blacklists",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessHistories_users_UserId",
                table: "AccessHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Blacklists_users_UserId",
                table: "Blacklists");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Blacklists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AccessHistories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessHistories_users_UserId",
                table: "AccessHistories",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blacklists_users_UserId",
                table: "Blacklists",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
