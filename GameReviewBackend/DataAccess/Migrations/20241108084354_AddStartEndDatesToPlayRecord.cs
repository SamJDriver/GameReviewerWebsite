using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddStartEndDatesToPlayRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "end_date",
                table: "play_records",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_updated_date",
                table: "play_records",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "start_date",
                table: "play_records",
                type: "datetime",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 11, 8, 0, 43, 54, 219, DateTimeKind.Local).AddTicks(3120));

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 11, 8, 0, 43, 54, 219, DateTimeKind.Local).AddTicks(3150));

            migrationBuilder.UpdateData(
                table: "user_relationship_type_lookup",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 11, 8, 0, 43, 54, 219, DateTimeKind.Local).AddTicks(3220));

            migrationBuilder.UpdateData(
                table: "user_relationship_type_lookup",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 11, 8, 0, 43, 54, 219, DateTimeKind.Local).AddTicks(3230));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "end_date",
                table: "play_records");

            migrationBuilder.DropColumn(
                name: "last_updated_date",
                table: "play_records");

            migrationBuilder.DropColumn(
                name: "start_date",
                table: "play_records");

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 9, 26, 23, 1, 33, 894, DateTimeKind.Local).AddTicks(4980));

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 9, 26, 23, 1, 33, 894, DateTimeKind.Local).AddTicks(5020));

            migrationBuilder.UpdateData(
                table: "user_relationship_type_lookup",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 9, 26, 23, 1, 33, 894, DateTimeKind.Local).AddTicks(5090));

            migrationBuilder.UpdateData(
                table: "user_relationship_type_lookup",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 9, 26, 23, 1, 33, 894, DateTimeKind.Local).AddTicks(5090));
        }
    }
}
