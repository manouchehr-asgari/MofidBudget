using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MofidBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BeneficiaryEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeneficiaryEmplyees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployeeCount = table.Column<int>(type: "int", nullable: false),
                    BeneficiaryId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeneficiaryEmplyees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeneficiaryEmplyees_Beneficiaries_BeneficiaryId",
                        column: x => x.BeneficiaryId,
                        principalTable: "Beneficiaries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiaryEmplyees_BeneficiaryId",
                table: "BeneficiaryEmplyees",
                column: "BeneficiaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeneficiaryEmplyees");
        }
    }
}
