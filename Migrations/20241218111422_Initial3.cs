using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProgrammasy.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CorrectAnswedId",
                table: "Questions",
                newName: "CorrectAnswedIndex");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CorrectAnswedIndex",
                table: "Questions",
                newName: "CorrectAnswedId");
        }
    }
}
