using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUserIdFromPlayRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "id10",
                table: "play_records");

            migrationBuilder.DropIndex(
                name: "user_id1",
                table: "play_records");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "play_records");

            migrationBuilder.RenameIndex(
                name: "id12",
                table: "users",
                newName: "id11");

            migrationBuilder.RenameIndex(
                name: "id11",
                table: "user_relationship_type_lookup",
                newName: "id10");

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 7, 25, 0, 43, 5, 789, DateTimeKind.Local).AddTicks(4960));

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 7, 25, 0, 43, 5, 789, DateTimeKind.Local).AddTicks(5000));

            migrationBuilder.UpdateData(
                table: "user_relationship_type_lookup",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 7, 25, 0, 43, 5, 789, DateTimeKind.Local).AddTicks(5090));

            migrationBuilder.UpdateData(
                table: "user_relationship_type_lookup",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 7, 25, 0, 43, 5, 789, DateTimeKind.Local).AddTicks(5090));

            migrationBuilder.CreateIndex(
                name: "created_by1",
                table: "play_records",
                column: "created_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "created_by1",
                table: "play_records");

            migrationBuilder.RenameIndex(
                name: "id11",
                table: "users",
                newName: "id12");

            migrationBuilder.RenameIndex(
                name: "id10",
                table: "user_relationship_type_lookup",
                newName: "id11");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "play_records",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "",
                collation: "utf8mb4_uca1400_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 7, 21, 16, 31, 13, 155, DateTimeKind.Local).AddTicks(5472));

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 7, 21, 16, 31, 13, 155, DateTimeKind.Local).AddTicks(5508));

            migrationBuilder.UpdateData(
                table: "user_relationship_type_lookup",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 7, 21, 16, 31, 13, 155, DateTimeKind.Local).AddTicks(5636));

            migrationBuilder.UpdateData(
                table: "user_relationship_type_lookup",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 7, 21, 16, 31, 13, 155, DateTimeKind.Local).AddTicks(5639));

            migrationBuilder.CreateIndex(
                name: "id10",
                table: "play_records",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "user_id1",
                table: "play_records",
                column: "user_id");
        }
    }
}
