using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddParentIdToGamesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 7, 10, 14, 10, 28, 454, DateTimeKind.Local).AddTicks(4750));

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 7, 10, 14, 10, 28, 454, DateTimeKind.Local).AddTicks(4784));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 7, 10, 14, 7, 6, 48, DateTimeKind.Local).AddTicks(5199));

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 7, 10, 14, 7, 6, 48, DateTimeKind.Local).AddTicks(5238));
        }
    }
}
