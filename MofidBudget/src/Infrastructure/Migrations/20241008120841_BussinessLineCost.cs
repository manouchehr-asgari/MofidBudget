using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MofidBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BussinessLineCost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BussinessLine",
                table: "Costs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BussinessLine",
                table: "Costs");
        }
    }
}
