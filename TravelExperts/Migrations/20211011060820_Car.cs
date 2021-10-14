using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelExperts.Migrations
{
    public partial class Car : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgtPicture",
                table: "Agents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgncyPicture",
                table: "Agencies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgtPicture",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "AgncyPicture",
                table: "Agencies");
        }
    }
}
