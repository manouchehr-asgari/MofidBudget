using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MofidBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefractionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RefractionType",
                table: "BeneficiaryRelationShips",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefractionType",
                table: "BeneficiaryRelationShips");
        }
    }
}
