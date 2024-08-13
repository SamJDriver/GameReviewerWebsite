using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovedModifiedByAndObsolete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 7, 10, 14, 1, 16, 380, DateTimeKind.Local).AddTicks(8127));

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 7, 10, 14, 1, 16, 380, DateTimeKind.Local).AddTicks(8169));
        }
    }
}
