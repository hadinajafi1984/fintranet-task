using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace congestion.calculator.Migrations
{
    public partial class last_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "HoursAmounts");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "HoursAmounts");

            migrationBuilder.AddColumn<int>(
                name: "EndHour",
                table: "HoursAmounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EndMinute",
                table: "HoursAmounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartHour",
                table: "HoursAmounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartMinute",
                table: "HoursAmounts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndHour",
                table: "HoursAmounts");

            migrationBuilder.DropColumn(
                name: "EndMinute",
                table: "HoursAmounts");

            migrationBuilder.DropColumn(
                name: "StartHour",
                table: "HoursAmounts");

            migrationBuilder.DropColumn(
                name: "StartMinute",
                table: "HoursAmounts");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "HoursAmounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "HoursAmounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
