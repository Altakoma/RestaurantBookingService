using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class IdNameChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_User_UserId",
                table: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RefreshToken",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_User_Id",
                table: "RefreshToken",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_User_Id",
                table: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RefreshToken",
                newName: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_User_UserId",
                table: "RefreshToken",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
