using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Memcach.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateMemcach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TimeTaken",
                table: "User",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeTaken",
                table: "User");
        }
    }
}
