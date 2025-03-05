using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _0503_mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccess",
                table: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccess",
                table: "Books",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
