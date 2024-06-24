using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "id12",
                table: "users",
                newName: "id13");

            migrationBuilder.RenameIndex(
                name: "id11",
                table: "user_relationship_type_lookup",
                newName: "id12");

            migrationBuilder.RenameIndex(
                name: "id10",
                table: "user_relationship",
                newName: "id11");

            migrationBuilder.RenameIndex(
                name: "id9",
                table: "play_records",
                newName: "id10");

            migrationBuilder.RenameIndex(
                name: "game_id3",
                table: "play_records",
                newName: "game_id4");

            migrationBuilder.RenameIndex(
                name: "id8",
                table: "play_record_comments",
                newName: "id9");

            migrationBuilder.RenameIndex(
                name: "id7",
                table: "platforms",
                newName: "id8");

            migrationBuilder.RenameIndex(
                name: "id6",
                table: "genres_lookup",
                newName: "id7");

            migrationBuilder.RenameIndex(
                name: "id5",
                table: "games_platforms_link",
                newName: "id6");

            migrationBuilder.RenameIndex(
                name: "game_id2",
                table: "games_platforms_link",
                newName: "game_id3");

            migrationBuilder.RenameIndex(
                name: "id4",
                table: "games_genres_lookup_link",
                newName: "id5");

            migrationBuilder.RenameIndex(
                name: "game_id1",
                table: "games_genres_lookup_link",
                newName: "game_id2");

            migrationBuilder.RenameIndex(
                name: "id3",
                table: "games_companies_link",
                newName: "id4");

            migrationBuilder.RenameIndex(
                name: "id2",
                table: "games",
                newName: "id3");

            migrationBuilder.CreateTable(
                name: "cover",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    game_id = table.Column<int>(type: "int(11)", nullable: false),
                    alpha_channel_flag = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    animated_flag = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    height = table.Column<int>(type: "int(11)", nullable: false),
                    width = table.Column<int>(type: "int(11)", nullable: false),
                    image_url = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cover", x => x.id);
                    table.ForeignKey(
                        name: "cover_games_ibfk_1",
                        column: x => x.game_id,
                        principalTable: "games",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "game_id1",
                table: "cover",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "id2",
                table: "cover",
                column: "id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cover");

            migrationBuilder.RenameIndex(
                name: "id13",
                table: "users",
                newName: "id12");

            migrationBuilder.RenameIndex(
                name: "id12",
                table: "user_relationship_type_lookup",
                newName: "id11");

            migrationBuilder.RenameIndex(
                name: "id11",
                table: "user_relationship",
                newName: "id10");

            migrationBuilder.RenameIndex(
                name: "id10",
                table: "play_records",
                newName: "id9");

            migrationBuilder.RenameIndex(
                name: "game_id4",
                table: "play_records",
                newName: "game_id3");

            migrationBuilder.RenameIndex(
                name: "id9",
                table: "play_record_comments",
                newName: "id8");

            migrationBuilder.RenameIndex(
                name: "id8",
                table: "platforms",
                newName: "id7");

            migrationBuilder.RenameIndex(
                name: "id7",
                table: "genres_lookup",
                newName: "id6");

            migrationBuilder.RenameIndex(
                name: "id6",
                table: "games_platforms_link",
                newName: "id5");

            migrationBuilder.RenameIndex(
                name: "game_id3",
                table: "games_platforms_link",
                newName: "game_id2");

            migrationBuilder.RenameIndex(
                name: "id5",
                table: "games_genres_lookup_link",
                newName: "id4");

            migrationBuilder.RenameIndex(
                name: "game_id2",
                table: "games_genres_lookup_link",
                newName: "game_id1");

            migrationBuilder.RenameIndex(
                name: "id4",
                table: "games_companies_link",
                newName: "id3");

            migrationBuilder.RenameIndex(
                name: "id3",
                table: "games",
                newName: "id2");
        }
    }
}
