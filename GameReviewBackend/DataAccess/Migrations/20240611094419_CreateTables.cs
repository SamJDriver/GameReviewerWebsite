using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    founded_date = table.Column<DateOnly>(type: "date", nullable: false),
                    image_file_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    developer_flag = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    publisher_flag = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_by = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    release_date = table.Column<DateOnly>(type: "date", nullable: false),
                    image_file_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "genres_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "mediumtext", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres_lookup", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "platforms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    release_date = table.Column<DateOnly>(type: "date", nullable: false),
                    image_file_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_platforms", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "user_relationship_type_lookup",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_relationship_type_lookup", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password_hash = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    salt = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    admin_flag = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image_file_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "games_companies_link",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    games_id = table.Column<int>(type: "int(11)", nullable: false),
                    companies_id = table.Column<int>(type: "int(11)", nullable: false),
                    developer_flag = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    publisher_flag = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    porting_flag = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    supporting_flag = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_by = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games_companies_link", x => x.id);
                    table.ForeignKey(
                        name: "games_companies_link_ibfk_1",
                        column: x => x.games_id,
                        principalTable: "games",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "games_companies_link_ibfk_2",
                        column: x => x.companies_id,
                        principalTable: "companies",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "games_genres_lookup_link",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    game_id = table.Column<int>(type: "int(11)", nullable: false),
                    genre_lookup_id = table.Column<int>(type: "int(11)", nullable: false),
                    created_by = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games_genres_lookup_link", x => x.id);
                    table.ForeignKey(
                        name: "games_genres_lookup_link_ibfk_1",
                        column: x => x.game_id,
                        principalTable: "games",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "games_genres_lookup_link_ibfk_2",
                        column: x => x.genre_lookup_id,
                        principalTable: "genres_lookup",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "games_platforms_link",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    game_id = table.Column<int>(type: "int(11)", nullable: false),
                    platform_id = table.Column<int>(type: "int(11)", nullable: false),
                    release_date = table.Column<DateOnly>(type: "date", nullable: true),
                    created_by = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games_platforms_link", x => x.id);
                    table.ForeignKey(
                        name: "games_platforms_link_ibfk_1",
                        column: x => x.game_id,
                        principalTable: "games",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "games_platforms_link_ibfk_2",
                        column: x => x.platform_id,
                        principalTable: "platforms",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "play_records",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int(11)", nullable: false),
                    game_id = table.Column<int>(type: "int(11)", nullable: false),
                    completed_flag = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    hours_played = table.Column<int>(type: "int(11)", nullable: true),
                    rating = table.Column<int>(type: "int(11)", nullable: true),
                    play_description = table.Column<string>(type: "mediumtext", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_play_records", x => x.id);
                    table.ForeignKey(
                        name: "play_records_ibfk_1",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "play_records_ibfk_2",
                        column: x => x.game_id,
                        principalTable: "games",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "user_relationship",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int(11)", nullable: false),
                    friend_id = table.Column<int>(type: "int(11)", nullable: false),
                    relationship_type_lookup_id = table.Column<int>(type: "int(11)", nullable: false),
                    created_by = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_relationship", x => x.id);
                    table.ForeignKey(
                        name: "user_relationship_ibfk_1",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "user_relationship_ibfk_2",
                        column: x => x.friend_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "user_relationship_ibfk_3",
                        column: x => x.relationship_type_lookup_id,
                        principalTable: "user_relationship_type_lookup",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "play_record_comments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int(11)", nullable: false),
                    play_record_id = table.Column<int>(type: "int(11)", nullable: false),
                    comment_text = table.Column<string>(type: "mediumtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    upvote_count = table.Column<int>(type: "int(11)", nullable: false),
                    downvote_count = table.Column<int>(type: "int(11)", nullable: false),
                    created_by = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_play_record_comments", x => x.id);
                    table.ForeignKey(
                        name: "play_record_comments_ibfk_1",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "play_record_comments_ibfk_2",
                        column: x => x.play_record_id,
                        principalTable: "play_records",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "id",
                table: "companies",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id1",
                table: "games",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "companies_id",
                table: "games_companies_link",
                column: "companies_id");

            migrationBuilder.CreateIndex(
                name: "games_id",
                table: "games_companies_link",
                column: "games_id");

            migrationBuilder.CreateIndex(
                name: "id2",
                table: "games_companies_link",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "game_id",
                table: "games_genres_lookup_link",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "genre_lookup_id",
                table: "games_genres_lookup_link",
                column: "genre_lookup_id");

            migrationBuilder.CreateIndex(
                name: "id3",
                table: "games_genres_lookup_link",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "game_id1",
                table: "games_platforms_link",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "id4",
                table: "games_platforms_link",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "platform_id",
                table: "games_platforms_link",
                column: "platform_id");

            migrationBuilder.CreateIndex(
                name: "id5",
                table: "genres_lookup",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id6",
                table: "platforms",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id7",
                table: "play_record_comments",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "play_record_id",
                table: "play_record_comments",
                column: "play_record_id");

            migrationBuilder.CreateIndex(
                name: "user_id",
                table: "play_record_comments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "game_id2",
                table: "play_records",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "id8",
                table: "play_records",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "user_id1",
                table: "play_records",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "friend_id",
                table: "user_relationship",
                column: "friend_id");

            migrationBuilder.CreateIndex(
                name: "id9",
                table: "user_relationship",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "relationship_type_lookup_id",
                table: "user_relationship",
                column: "relationship_type_lookup_id");

            migrationBuilder.CreateIndex(
                name: "user_id2",
                table: "user_relationship",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "id10",
                table: "user_relationship_type_lookup",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id11",
                table: "users",
                column: "id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "games_companies_link");

            migrationBuilder.DropTable(
                name: "games_genres_lookup_link");

            migrationBuilder.DropTable(
                name: "games_platforms_link");

            migrationBuilder.DropTable(
                name: "play_record_comments");

            migrationBuilder.DropTable(
                name: "user_relationship");

            migrationBuilder.DropTable(
                name: "companies");

            migrationBuilder.DropTable(
                name: "genres_lookup");

            migrationBuilder.DropTable(
                name: "platforms");

            migrationBuilder.DropTable(
                name: "play_records");

            migrationBuilder.DropTable(
                name: "user_relationship_type_lookup");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "games");
        }
    }
}
