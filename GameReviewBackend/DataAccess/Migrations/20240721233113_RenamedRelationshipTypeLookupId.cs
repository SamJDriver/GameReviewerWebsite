using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenamedRelationshipTypeLookupId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "user_relationship_ibfk_3",
                table: "user_relationship");

            migrationBuilder.DropIndex(
                name: "child_user_id",
                table: "user_relationship");

            migrationBuilder.DropIndex(
                name: "id11",
                table: "user_relationship");

            migrationBuilder.DropIndex(
                name: "user_id2",
                table: "user_relationship");

            migrationBuilder.RenameIndex(
                name: "id13",
                table: "users",
                newName: "id12");

            migrationBuilder.RenameIndex(
                name: "id12",
                table: "user_relationship_type_lookup",
                newName: "id11");

            migrationBuilder.RenameColumn(
                name: "relationship_type_lookup_id",
                table: "user_relationship",
                newName: "user_relationship_type_lookup_id");

            migrationBuilder.RenameIndex(
                name: "relationship_type_lookup_id",
                table: "user_relationship",
                newName: "user_relationship_ibfk_1");

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

            migrationBuilder.AddForeignKey(
                name: "user_relationship_ibfk_1",
                table: "user_relationship",
                column: "user_relationship_type_lookup_id",
                principalTable: "user_relationship_type_lookup",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "user_relationship_ibfk_1",
                table: "user_relationship");

            migrationBuilder.RenameIndex(
                name: "id12",
                table: "users",
                newName: "id13");

            migrationBuilder.RenameIndex(
                name: "id11",
                table: "user_relationship_type_lookup",
                newName: "id12");

            migrationBuilder.RenameColumn(
                name: "user_relationship_type_lookup_id",
                table: "user_relationship",
                newName: "relationship_type_lookup_id");

            migrationBuilder.RenameIndex(
                name: "user_relationship_ibfk_1",
                table: "user_relationship",
                newName: "relationship_type_lookup_id");

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

            migrationBuilder.UpdateData(
                table: "user_relationship_type_lookup",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 7, 14, 19, 29, 25, 441, DateTimeKind.Local).AddTicks(4292));

            migrationBuilder.UpdateData(
                table: "user_relationship_type_lookup",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 7, 14, 19, 29, 25, 441, DateTimeKind.Local).AddTicks(4296));

            migrationBuilder.CreateIndex(
                name: "child_user_id",
                table: "user_relationship",
                column: "child_user_id");

            migrationBuilder.CreateIndex(
                name: "id11",
                table: "user_relationship",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "user_id2",
                table: "user_relationship",
                column: "created_by");

            migrationBuilder.AddForeignKey(
                name: "user_relationship_ibfk_3",
                table: "user_relationship",
                column: "relationship_type_lookup_id",
                principalTable: "user_relationship_type_lookup",
                principalColumn: "id");
        }
    }
}
