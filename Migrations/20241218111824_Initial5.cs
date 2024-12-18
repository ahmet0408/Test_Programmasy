using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProgrammasy.Migrations
{
    public partial class Initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CorrectAnswedIndex",
                table: "Questions",
                newName: "CorrectAnswerIndex");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CorrectAnswerIndex",
                table: "Questions",
                newName: "CorrectAnswedIndex");
        }
    }
}
