using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evacuation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixModelSync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "EstimatedArrival",
                table: "Assignments",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EstimatedArrival",
                table: "Assignments",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
