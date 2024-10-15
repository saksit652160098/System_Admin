using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Memcach.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateMemcachOS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TimeTaken",
                table: "User",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TimeTaken",
                table: "User",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");
        }
    }
}
