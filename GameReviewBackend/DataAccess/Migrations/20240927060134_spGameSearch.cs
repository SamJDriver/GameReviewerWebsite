using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class spGameSearch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
          var sp = @"
          DROP PROCEDURE IF EXISTS spGameSearch;
          DELIMITER $$
          CREATE PROCEDURE spGameSearch(
              IN pSearchTerm VARCHAR(255)
              , IN pGenreIds VARCHAR(255)
              , IN pStartReleaseDate DATE
              , IN pEndReleaseDate DATE
              , IN pPageIndex INT
              , IN pPageSize INT
          )
            BEGIN
              DECLARE V_Max INT;
              DECLARE V_Min INT DEFAULT 1;
              DECLARE V_Str VARCHAR(50) DEFAULT '';
          	DECLARE pOffset INT;

              DECLARE pString VARCHAR(500);
              SET pString = (CASE WHEN RIGHT(pGenreIds, 1) = ',' THEN pGenreIds ELSE CONCAT(pGenreIds, ',') END);
              SET V_Max = LENGTH(pGenreIds) - LENGTH(REPLACE(pGenreIds, ',', '')) + 1;

              DROP TABLE IF EXISTS Temp;
              CREATE TEMPORARY TABLE Temp(Slno INT PRIMARY KEY AUTO_INCREMENT, Item VARCHAR(50));


              WHILE V_Min <= V_Max DO
          	  SET V_Str = SUBSTRING(pString, 1, INSTR(pString, ',') - 1);

          	  INSERT INTO Temp(Item) VALUES(V_Str);
          	  SET pString = SUBSTRING(pString, INSTR(pString, ',') + 1);
          	  SET V_Min = V_Min + 1;
              END WHILE;

              SET pOffset = pPageSize * pPageIndex;

              SELECT
                games.id,
                games.title,
                games.release_date,
                games.description,
                games.created_by,
                games.created_date,
                games.parent_game_id
              FROM
                 games 
          	JOIN
                games_genres_lookup_link
                ON games.id = games_genres_lookup_link.game_id
          	GROUP BY
                games.id
          	HAVING
                COUNT(DISTINCT games_genres_lookup_link.genre_lookup_id) >= (SELECT COUNT(*) FROM Temp)
                AND COUNT(DISTINCT CASE WHEN games_genres_lookup_link.genre_lookup_id IN (SELECT Item FROM Temp) THEN games_genres_lookup_link.genre_lookup_id END) >= (SELECT COUNT(*) FROM Temp)
          	ORDER BY
                games.release_date DESC
          	LIMIT pPageSize OFFSET pOffset;

              DROP TABLE IF EXISTS Temp;
          END$$
          DELIMITER ;
          ";
        
        migrationBuilder.Sql(sp);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
