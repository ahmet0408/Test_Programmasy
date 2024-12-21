using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProgrammasy.Migrations
{
    public partial class Desc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TestTitle",
                table: "TestResults",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TestResults",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TestResults");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TestResults",
                newName: "TestTitle");
        }
    }
}
