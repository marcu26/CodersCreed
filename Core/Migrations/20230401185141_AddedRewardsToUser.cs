﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class AddedRewardsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RewardUser",
                columns: table => new
                {
                    RewardsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardUser", x => new { x.RewardsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RewardUser_Rewards_RewardsId",
                        column: x => x.RewardsId,
                        principalTable: "Rewards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RewardUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2023, 4, 1, 21, 51, 41, 458, DateTimeKind.Local).AddTicks(2455));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2023, 4, 1, 21, 51, 41, 458, DateTimeKind.Local).AddTicks(2486));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2023, 4, 1, 21, 51, 41, 458, DateTimeKind.Local).AddTicks(2488));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2023, 4, 1, 21, 51, 41, 458, DateTimeKind.Local).AddTicks(2489));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2023, 4, 1, 21, 51, 41, 458, DateTimeKind.Local).AddTicks(2570));

            migrationBuilder.CreateIndex(
                name: "IX_RewardUser_UsersId",
                table: "RewardUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RewardUser");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2023, 4, 1, 20, 25, 48, 240, DateTimeKind.Local).AddTicks(8480));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2023, 4, 1, 20, 25, 48, 240, DateTimeKind.Local).AddTicks(8513));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2023, 4, 1, 20, 25, 48, 240, DateTimeKind.Local).AddTicks(8515));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2023, 4, 1, 20, 25, 48, 240, DateTimeKind.Local).AddTicks(8516));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2023, 4, 1, 20, 25, 48, 240, DateTimeKind.Local).AddTicks(8602));
        }
    }
}
