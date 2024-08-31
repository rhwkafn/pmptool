using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pm.api.Migrations
{
    public partial class annia0813 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Tasks",
                newName: "StarttaskDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndtaskDate",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CompletedTasks",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalTasks",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndtaskDate",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CompletedTasks",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "TotalTasks",
                table: "Project");

            migrationBuilder.RenameColumn(
                name: "StarttaskDate",
                table: "Tasks",
                newName: "DueDate");
        }
    }
}
