using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProgrammasy.Migrations
{
    public partial class Initial12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Passed",
                table: "TestResults");

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "TestResults",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Percentage",
                table: "TestResults",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "TestResults",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TestTitle",
                table: "TestResults",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalPoints",
                table: "TestResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "TestResults");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "TestResults");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "TestResults");

            migrationBuilder.DropColumn(
                name: "TestTitle",
                table: "TestResults");

            migrationBuilder.DropColumn(
                name: "TotalPoints",
                table: "TestResults");

            migrationBuilder.AddColumn<bool>(
                name: "Passed",
                table: "TestResults",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
