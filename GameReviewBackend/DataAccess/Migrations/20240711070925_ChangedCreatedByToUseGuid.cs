using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangedCreatedByToUseGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "user_relationship_ibfk_1",
                table: "user_relationship");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "users",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "user_relationship_type_lookup",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "user_relationship",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(int),
                oldType: "int(11)")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "user_relationship",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "play_records",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "char(16)",
                oldFixedLength: true,
                oldMaxLength: 16)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "play_records",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "play_record_comments",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "platforms",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "genres_lookup",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "games_platforms_link",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "games_genres_lookup_link",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "games_companies_link",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "games",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "game_self_link_type_lookup",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "game_self_link",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "cover",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "companies",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "artwork",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25)
                .Annotation("MySql:CharSet", "utf8mb4")
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "users",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "user_relationship_type_lookup",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "user_relationship",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "user_relationship",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "play_records",
                type: "char(16)",
                fixedLength: true,
                maxLength: 16,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "play_records",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "play_record_comments",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "platforms",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "genres_lookup",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "games_platforms_link",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "games_genres_lookup_link",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "games_companies_link",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "games",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "game_self_link_type_lookup",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "game_self_link",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "cover",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "companies",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "artwork",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                collation: "utf8mb4_uca1400_ai_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

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

            migrationBuilder.AddForeignKey(
                name: "user_relationship_ibfk_1",
                table: "user_relationship",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
