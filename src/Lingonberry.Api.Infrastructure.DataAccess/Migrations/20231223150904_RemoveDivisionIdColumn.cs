using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lingonberry.Api.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDivisionIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "Departments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DivisionId",
                table: "Departments",
                type: "integer",
                nullable: true);
        }
    }
}
