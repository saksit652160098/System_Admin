using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Memcach.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateMemcachmi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MemcachedTimeTaken",
                table: "User",
                newName: "CacheTimeTaken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CacheTimeTaken",
                table: "User",
                newName: "MemcachedTimeTaken");
        }
    }
}
