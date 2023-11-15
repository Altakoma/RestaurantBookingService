using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SchemeChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeRestaurant");

            migrationBuilder.DropTable(
                name: "MenuRestaurant");

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Menu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Menu_RestaurantId",
                table: "Menu",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_RestaurantId",
                table: "Employee",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Restaurant_RestaurantId",
                table: "Employee",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Restaurant_RestaurantId",
                table: "Menu",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Restaurant_RestaurantId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Restaurant_RestaurantId",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Menu_RestaurantId",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Employee_RestaurantId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Employee");

            migrationBuilder.CreateTable(
                name: "EmployeeRestaurant",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    RestaurantsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRestaurant", x => new { x.EmployeesId, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_EmployeeRestaurant_Employee_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeRestaurant_Restaurant_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuRestaurant",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    RestaurantsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuRestaurant", x => new { x.MenuId, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_MenuRestaurant_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuRestaurant_Restaurant_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRestaurant_RestaurantsId",
                table: "EmployeeRestaurant",
                column: "RestaurantsId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRestaurant_RestaurantsId",
                table: "MenuRestaurant",
                column: "RestaurantsId");
        }
    }
}
