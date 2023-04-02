using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class AddedMandatoryCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isMandatory",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2023, 4, 2, 6, 45, 9, 749, DateTimeKind.Local).AddTicks(3523));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2023, 4, 2, 6, 45, 9, 749, DateTimeKind.Local).AddTicks(3560));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2023, 4, 2, 6, 45, 9, 749, DateTimeKind.Local).AddTicks(3562));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2023, 4, 2, 6, 45, 9, 749, DateTimeKind.Local).AddTicks(3564));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2023, 4, 2, 6, 45, 9, 749, DateTimeKind.Local).AddTicks(3663));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isMandatory",
                table: "Courses");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2023, 4, 2, 1, 56, 56, 408, DateTimeKind.Local).AddTicks(1177));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2023, 4, 2, 1, 56, 56, 408, DateTimeKind.Local).AddTicks(1224));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2023, 4, 2, 1, 56, 56, 408, DateTimeKind.Local).AddTicks(1228));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2023, 4, 2, 1, 56, 56, 408, DateTimeKind.Local).AddTicks(1231));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2023, 4, 2, 1, 56, 56, 408, DateTimeKind.Local).AddTicks(1394));
        }
    }
}
