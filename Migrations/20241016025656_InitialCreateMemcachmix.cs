using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Memcach.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateMemcachmix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MemcachedTimeTaken",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemcachedTimeTaken",
                table: "User");
        }
    }
}
