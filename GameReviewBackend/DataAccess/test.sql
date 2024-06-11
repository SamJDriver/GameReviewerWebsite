CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `companies` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `founded_date` date NOT NULL,
    `image_file_path` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
    `developer_flag` tinyint(1) NOT NULL,
    `publisher_flag` tinyint(1) NOT NULL,
    `created_by` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `created_date` datetime NOT NULL,
    CONSTRAINT `PK_companies` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `games` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `title` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `release_date` date NOT NULL,
    `image_file_path` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `created_by` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `created_date` datetime NOT NULL,
    CONSTRAINT `PK_games` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `genres_lookup` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
    `code` varchar(8) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
    `description` mediumtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
    `created_by` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `created_date` datetime NOT NULL,
    CONSTRAINT `PK_genres_lookup` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `platforms` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `release_date` date NOT NULL,
    `image_file_path` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `created_by` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `created_date` datetime NOT NULL,
    CONSTRAINT `PK_platforms` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `user_relationship_type_lookup` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `code` varchar(8) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `created_by` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `created_date` datetime NOT NULL,
    CONSTRAINT `PK_user_relationship_type_lookup` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `users` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `username` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `password_hash` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `salt` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
    `admin_flag` tinyint(1) NOT NULL,
    `email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `image_file_path` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
    `created_by` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `created_date` datetime NOT NULL,
    CONSTRAINT `PK_users` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `games_companies_link` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `games_id` int(11) NOT NULL,
    `companies_id` int(11) NOT NULL,
    `developer_flag` tinyint(1) NOT NULL,
    `publisher_flag` tinyint(1) NOT NULL,
    `porting_flag` tinyint(1) NOT NULL,
    `supporting_flag` tinyint(1) NOT NULL,
    `created_by` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `created_date` datetime NOT NULL,
    CONSTRAINT `PK_games_companies_link` PRIMARY KEY (`id`),
    CONSTRAINT `games_companies_link_ibfk_1` FOREIGN KEY (`games_id`) REFERENCES `games` (`id`),
    CONSTRAINT `games_companies_link_ibfk_2` FOREIGN KEY (`companies_id`) REFERENCES `companies` (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `games_genres_lookup_link` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `game_id` int(11) NOT NULL,
    `genre_lookup_id` int(11) NOT NULL,
    `created_by` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `created_date` datetime NOT NULL,
    CONSTRAINT `PK_games_genres_lookup_link` PRIMARY KEY (`id`),
    CONSTRAINT `games_genres_lookup_link_ibfk_1` FOREIGN KEY (`game_id`) REFERENCES `games` (`id`),
    CONSTRAINT `games_genres_lookup_link_ibfk_2` FOREIGN KEY (`genre_lookup_id`) REFERENCES `genres_lookup` (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `games_platforms_link` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `game_id` int(11) NOT NULL,
    `platform_id` int(11) NOT NULL,
    `release_date` date NULL,
    `created_by` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `created_date` datetime NOT NULL,
    CONSTRAINT `PK_games_platforms_link` PRIMARY KEY (`id`),
    CONSTRAINT `games_platforms_link_ibfk_1` FOREIGN KEY (`game_id`) REFERENCES `games` (`id`),
    CONSTRAINT `games_platforms_link_ibfk_2` FOREIGN KEY (`platform_id`) REFERENCES `platforms` (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `play_records` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `user_id` int(11) NOT NULL,
    `game_id` int(11) NOT NULL,
    `completed_flag` tinyint(1) NULL,
    `hours_played` int(11) NULL,
    `rating` int(11) NULL,
    `play_description` mediumtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
    `created_by` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `created_date` datetime NOT NULL,
    CONSTRAINT `PK_play_records` PRIMARY KEY (`id`),
    CONSTRAINT `play_records_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`),
    CONSTRAINT `play_records_ibfk_2` FOREIGN KEY (`game_id`) REFERENCES `games` (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `user_relationship` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `user_id` int(11) NOT NULL,
    `friend_id` int(11) NOT NULL,
    `relationship_type_lookup_id` int(11) NOT NULL,
    `created_by` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `created_date` datetime NOT NULL,
    CONSTRAINT `PK_user_relationship` PRIMARY KEY (`id`),
    CONSTRAINT `user_relationship_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`),
    CONSTRAINT `user_relationship_ibfk_2` FOREIGN KEY (`friend_id`) REFERENCES `users` (`id`),
    CONSTRAINT `user_relationship_ibfk_3` FOREIGN KEY (`relationship_type_lookup_id`) REFERENCES `user_relationship_type_lookup` (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `play_record_comments` (
    `id` int(11) NOT NULL AUTO_INCREMENT,
    `user_id` int(11) NOT NULL,
    `play_record_id` int(11) NOT NULL,
    `comment_text` mediumtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `upvote_count` int(11) NOT NULL,
    `downvote_count` int(11) NOT NULL,
    `created_by` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
    `created_date` datetime NOT NULL,
    CONSTRAINT `PK_play_record_comments` PRIMARY KEY (`id`),
    CONSTRAINT `play_record_comments_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`),
    CONSTRAINT `play_record_comments_ibfk_2` FOREIGN KEY (`play_record_id`) REFERENCES `play_records` (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE UNIQUE INDEX `id` ON `companies` (`id`);

CREATE UNIQUE INDEX `id1` ON `games` (`id`);

CREATE INDEX `companies_id` ON `games_companies_link` (`companies_id`);

CREATE INDEX `games_id` ON `games_companies_link` (`games_id`);

CREATE UNIQUE INDEX `id2` ON `games_companies_link` (`id`);

CREATE INDEX `game_id` ON `games_genres_lookup_link` (`game_id`);

CREATE INDEX `genre_lookup_id` ON `games_genres_lookup_link` (`genre_lookup_id`);

CREATE UNIQUE INDEX `id3` ON `games_genres_lookup_link` (`id`);

CREATE INDEX `game_id1` ON `games_platforms_link` (`game_id`);

CREATE UNIQUE INDEX `id4` ON `games_platforms_link` (`id`);

CREATE INDEX `platform_id` ON `games_platforms_link` (`platform_id`);

CREATE UNIQUE INDEX `id5` ON `genres_lookup` (`id`);

CREATE UNIQUE INDEX `id6` ON `platforms` (`id`);

CREATE UNIQUE INDEX `id7` ON `play_record_comments` (`id`);

CREATE INDEX `play_record_id` ON `play_record_comments` (`play_record_id`);

CREATE INDEX `user_id` ON `play_record_comments` (`user_id`);

CREATE INDEX `game_id2` ON `play_records` (`game_id`);

CREATE UNIQUE INDEX `id8` ON `play_records` (`id`);

CREATE INDEX `user_id1` ON `play_records` (`user_id`);

CREATE INDEX `friend_id` ON `user_relationship` (`friend_id`);

CREATE UNIQUE INDEX `id9` ON `user_relationship` (`id`);

CREATE INDEX `relationship_type_lookup_id` ON `user_relationship` (`relationship_type_lookup_id`);

CREATE INDEX `user_id2` ON `user_relationship` (`user_id`);

CREATE UNIQUE INDEX `id10` ON `user_relationship_type_lookup` (`id`);

CREATE UNIQUE INDEX `id11` ON `users` (`id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240611094419_CreateTables', '8.0.6');

COMMIT;

