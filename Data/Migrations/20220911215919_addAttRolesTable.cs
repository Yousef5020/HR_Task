using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Task.Data.Migrations
{
    public partial class addAttRolesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EmploymentDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 11, 23, 59, 19, 519, DateTimeKind.Local).AddTicks(3921),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 9, 16, 38, 22, 233, DateTimeKind.Local).AddTicks(6827));

            migrationBuilder.CreateTable(
                name: "AttendanceRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinAbsenceDays = table.Column<int>(type: "int", nullable: false),
                    MaxAbsenceDays = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsBonus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceRoles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceRoles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EmploymentDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 9, 16, 38, 22, 233, DateTimeKind.Local).AddTicks(6827),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 11, 23, 59, 19, 519, DateTimeKind.Local).AddTicks(3921));
        }
    }
}
