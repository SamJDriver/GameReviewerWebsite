using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUpvoteDownvoteFromCommentRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "downvote_count",
                table: "play_record_comments");

            migrationBuilder.DropColumn(
                name: "upvote_count",
                table: "play_record_comments");

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 7, 13, 23, 18, 23, 106, DateTimeKind.Local).AddTicks(8442));

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 7, 13, 23, 18, 23, 106, DateTimeKind.Local).AddTicks(8478));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "downvote_count",
                table: "play_record_comments",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "upvote_count",
                table: "play_record_comments",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 7, 12, 0, 7, 30, 638, DateTimeKind.Local).AddTicks(9498));

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 7, 12, 0, 7, 30, 638, DateTimeKind.Local).AddTicks(9533));
        }
    }
}
