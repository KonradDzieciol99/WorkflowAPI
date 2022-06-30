using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkflowApi.Migrations
{
    public partial class littlechanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "PTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 22, 2, 17, 23, 804, DateTimeKind.Local).AddTicks(9739),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 22, 2, 13, 17, 852, DateTimeKind.Local).AddTicks(2236));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "PTasks",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 29, 2, 17, 22, 923, DateTimeKind.Local).AddTicks(7960),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 29, 2, 13, 16, 975, DateTimeKind.Local).AddTicks(8878));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "PTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 22, 2, 13, 17, 852, DateTimeKind.Local).AddTicks(2236),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 22, 2, 17, 23, 804, DateTimeKind.Local).AddTicks(9739));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "PTasks",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 6, 29, 2, 13, 16, 975, DateTimeKind.Local).AddTicks(8878),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 6, 29, 2, 17, 22, 923, DateTimeKind.Local).AddTicks(7960));
        }
    }
}
