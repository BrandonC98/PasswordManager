using Microsoft.EntityFrameworkCore.Migrations;

namespace PasswordManagerData.Migrations
{
    public partial class AddAllTablesToContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessHistory_users_UserId",
                table: "AccessHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Blacklist_users_UserId",
                table: "Blacklist");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterPassword_users_UserId",
                table: "MasterPassword");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MasterPassword",
                table: "MasterPassword");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blacklist",
                table: "Blacklist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccessHistory",
                table: "AccessHistory");

            migrationBuilder.RenameTable(
                name: "MasterPassword",
                newName: "MasterPasswords");

            migrationBuilder.RenameTable(
                name: "Blacklist",
                newName: "Blacklists");

            migrationBuilder.RenameTable(
                name: "AccessHistory",
                newName: "AccessHistories");

            migrationBuilder.RenameIndex(
                name: "IX_MasterPassword_UserId",
                table: "MasterPasswords",
                newName: "IX_MasterPasswords_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Blacklist_UserId",
                table: "Blacklists",
                newName: "IX_Blacklists_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessHistory_UserId",
                table: "AccessHistories",
                newName: "IX_AccessHistories_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MasterPasswords",
                table: "MasterPasswords",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blacklists",
                table: "Blacklists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccessHistories",
                table: "AccessHistories",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPasswords_users_UserId",
                table: "MasterPasswords",
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

            migrationBuilder.DropForeignKey(
                name: "FK_MasterPasswords_users_UserId",
                table: "MasterPasswords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MasterPasswords",
                table: "MasterPasswords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blacklists",
                table: "Blacklists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccessHistories",
                table: "AccessHistories");

            migrationBuilder.RenameTable(
                name: "MasterPasswords",
                newName: "MasterPassword");

            migrationBuilder.RenameTable(
                name: "Blacklists",
                newName: "Blacklist");

            migrationBuilder.RenameTable(
                name: "AccessHistories",
                newName: "AccessHistory");

            migrationBuilder.RenameIndex(
                name: "IX_MasterPasswords_UserId",
                table: "MasterPassword",
                newName: "IX_MasterPassword_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Blacklists_UserId",
                table: "Blacklist",
                newName: "IX_Blacklist_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessHistories_UserId",
                table: "AccessHistory",
                newName: "IX_AccessHistory_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MasterPassword",
                table: "MasterPassword",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blacklist",
                table: "Blacklist",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccessHistory",
                table: "AccessHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessHistory_users_UserId",
                table: "AccessHistory",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blacklist_users_UserId",
                table: "Blacklist",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPassword_users_UserId",
                table: "MasterPassword",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
