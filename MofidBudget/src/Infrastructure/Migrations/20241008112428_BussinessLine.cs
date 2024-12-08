using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MofidBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BussinessLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ToBussinessLine",
                table: "BeneficiaryRelationShips",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToBussinessLine",
                table: "BeneficiaryRelationShips");
        }
    }
}
