using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkflowApi.Migrations
{
    public partial class littlechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "PTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 22, 2, 13, 17, 852, DateTimeKind.Local).AddTicks(2236),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 21, 21, 4, 13, 11, DateTimeKind.Local).AddTicks(1220));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "PTasks",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 29, 2, 13, 16, 975, DateTimeKind.Local).AddTicks(8878),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 28, 21, 4, 12, 156, DateTimeKind.Local).AddTicks(6957));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "PTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 21, 21, 4, 13, 11, DateTimeKind.Local).AddTicks(1220),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 22, 2, 13, 17, 852, DateTimeKind.Local).AddTicks(2236));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "PTasks",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 28, 21, 4, 12, 156, DateTimeKind.Local).AddTicks(6957),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 29, 2, 13, 16, 975, DateTimeKind.Local).AddTicks(8878));
        }
    }
}
