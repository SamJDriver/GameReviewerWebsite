using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FriendsUseEntraId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "user_relationship_ibfk_2",
                table: "user_relationship");

            migrationBuilder.DropIndex(
                name: "friend_id",
                table: "user_relationship");

            migrationBuilder.DropColumn(
                name: "friend_id",
                table: "user_relationship");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "user_relationship",
                newName: "child_user_id");

            migrationBuilder.RenameIndex(
                name: "user_id2",
                table: "user_relationship",
                newName: "child_user_id");

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 7, 14, 19, 29, 25, 441, DateTimeKind.Local).AddTicks(4117));

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 7, 14, 19, 29, 25, 441, DateTimeKind.Local).AddTicks(4159));

            migrationBuilder.InsertData(
                table: "user_relationship_type_lookup",
                columns: new[] { "id", "code", "created_by", "created_date", "description", "name" },
                values: new object[,]
                {
                    { 1, "FRIEND", "181972b7-1d32-4b26-bd1f-0bfc7b9d9f9f", new DateTime(2024, 7, 14, 19, 29, 25, 441, DateTimeKind.Local).AddTicks(4292), "One directional friend link", "Friend" },
                    { 2, "IGNORE", "181972b7-1d32-4b26-bd1f-0bfc7b9d9f9f", new DateTime(2024, 7, 14, 19, 29, 25, 441, DateTimeKind.Local).AddTicks(4296), "One directional ignore.", "Ignore" }
                });

            migrationBuilder.CreateIndex(
                name: "user_id2",
                table: "user_relationship",
                column: "created_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "user_id2",
                table: "user_relationship");

            migrationBuilder.DeleteData(
                table: "user_relationship_type_lookup",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "user_relationship_type_lookup",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "child_user_id",
                table: "user_relationship",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "child_user_id",
                table: "user_relationship",
                newName: "user_id2");

            migrationBuilder.AddColumn<int>(
                name: "friend_id",
                table: "user_relationship",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "friend_id",
                table: "user_relationship",
                column: "friend_id");

            migrationBuilder.AddForeignKey(
                name: "user_relationship_ibfk_2",
                table: "user_relationship",
                column: "friend_id",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
