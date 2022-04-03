using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SADA.Infrastructure.EF.Migrations
{
    public partial class EditCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "ShoppingCart",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "ShoppingCart",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "ShoppingCart");
        }
    }
}
