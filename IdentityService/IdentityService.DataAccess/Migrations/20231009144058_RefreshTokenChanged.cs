using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "RefreshToken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "RefreshToken",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
