using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BalanceLife.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStepRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StepRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Steps = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<double>(type: "float(6)", precision: 6, scale: 2, nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    ActiveTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepRecord", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StepRecord");
        }
    }
}
