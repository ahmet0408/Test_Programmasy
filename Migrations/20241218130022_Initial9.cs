using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProgrammasy.Migrations
{
    public partial class Initial9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Tests");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Tests",
                newName: "UpdatetAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatetAt",
                table: "Tests",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Tests",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
