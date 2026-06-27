using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BalanceLife.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMealLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealLogs_Meals_MealId",
                table: "MealLogs");

            migrationBuilder.DropIndex(
                name: "IX_MealLogs_MealId",
                table: "MealLogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MealLogs_MealId",
                table: "MealLogs",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealLogs_Meals_MealId",
                table: "MealLogs",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
