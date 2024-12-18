using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProgrammasy.Migrations
{
    public partial class Initial6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeLimit",
                table: "Tests",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeLimit",
                table: "Tests");
        }
    }
}
