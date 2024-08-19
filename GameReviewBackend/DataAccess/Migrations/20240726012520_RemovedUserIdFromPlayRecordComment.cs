using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUserIdFromPlayRecordComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "user_id",
                table: "play_record_comments");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "play_record_comments");

            migrationBuilder.RenameIndex(
                name: "created_by1",
                table: "play_records",
                newName: "created_by2");

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 7, 25, 18, 25, 20, 210, DateTimeKind.Local).AddTicks(7790));

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 7, 25, 18, 25, 20, 210, DateTimeKind.Local).AddTicks(7840));

            migrationBuilder.UpdateData(
                table: "user_relationship_type_lookup",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 7, 25, 18, 25, 20, 210, DateTimeKind.Local).AddTicks(7910));

            migrationBuilder.UpdateData(
                table: "user_relationship_type_lookup",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 7, 25, 18, 25, 20, 210, DateTimeKind.Local).AddTicks(7920));

            migrationBuilder.CreateIndex(
                name: "created_by1",
                table: "play_record_comments",
                column: "created_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "created_by1",
                table: "play_record_comments");

            migrationBuilder.RenameIndex(
                name: "created_by2",
                table: "play_records",
                newName: "created_by1");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "play_record_comments",
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
                name: "user_id",
                table: "play_record_comments",
                column: "user_id");
        }
    }
}
