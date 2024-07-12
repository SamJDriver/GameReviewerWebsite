using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CommentUpvote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "play_record_comments_ibfk_1",
                table: "play_record_comments");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "play_record_comments",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(int),
                oldType: "int(11)")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "play_record_comment_vote",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    play_record_comment_id = table.Column<int>(type: "int(11)", nullable: false),
                    numerical_value = table.Column<int>(type: "int(11)", nullable: true),
                    emoji_value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_play_record_comment_vote", x => x.id);
                    table.ForeignKey(
                        name: "play_record_comment_vote_ibfk_1",
                        column: x => x.play_record_comment_id,
                        principalTable: "play_record_comments",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

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

            migrationBuilder.CreateIndex(
                name: "created_by",
                table: "play_record_comment_vote",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "play_record_comment_id",
                table: "play_record_comment_vote",
                column: "play_record_comment_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "play_record_comment_vote");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "play_record_comments",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2024, 7, 11, 0, 9, 24, 734, DateTimeKind.Local).AddTicks(7345));

            migrationBuilder.UpdateData(
                table: "game_self_link_type_lookup",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2024, 7, 11, 0, 9, 24, 734, DateTimeKind.Local).AddTicks(7388));

            migrationBuilder.AddForeignKey(
                name: "play_record_comments_ibfk_1",
                table: "play_record_comments",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
