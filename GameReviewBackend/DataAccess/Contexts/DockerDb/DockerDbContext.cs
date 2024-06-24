using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DataAccess.Models.DockerDb;
using Pomelo.EntityFrameworkCore.MySql;

namespace DataAccess.Contexts.DockerDb
{
    public partial class DockerDbContext : DbContext
    {
        public virtual DbSet<Companies> Companies { get; set; } = null!;
        public virtual DbSet<Games> Games { get; set; } = null!;
        public virtual DbSet<GamesCompaniesLink> GamesCompaniesLink { get; set; } = null!;
        public virtual DbSet<GamesGenresLookupLink> GamesGenresLookupLink { get; set; } = null!;
        public virtual DbSet<GamesPlatformsLink> GamesPlatformsLink { get; set; } = null!;
        public virtual DbSet<GenresLookup> GenresLookup { get; set; } = null!;
        public virtual DbSet<Platforms> Platforms { get; set; } = null!;
        public virtual DbSet<PlayRecordComments> PlayRecordComments { get; set; } = null!;
        public virtual DbSet<PlayRecords> PlayRecords { get; set; } = null!;
        public virtual DbSet<UserRelationship> UserRelationship { get; set; } = null!;
        public virtual DbSet<UserRelationshipTypeLookup> UserRelationshipTypeLookup { get; set; } = null!;
        public virtual DbSet<Users> Users { get; set; } = null!;
        public virtual DbSet<Artwork> Artwork { get; set; } = null!;

        public DockerDbContext()
        {
        }

        public DockerDbContext(DbContextOptions<DockerDbContext> options) : base(options)
        {
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {


        //     var serverName = Environment.GetEnvironmentVariable("MYSQL_HOST");
        //     var port = Environment.GetEnvironmentVariable("MYSQL_PORT");
        //     var databaseName = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
        //     var username = Environment.GetEnvironmentVariable("MYSQL_USER");
        //     var password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");

        //     var connectionString = ;
        //     optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<GamesCompaniesLink>(entity =>
            {
                entity.HasOne(d => d.Companies)
                    .WithMany(p => p.GamesCompaniesLink)
                    .HasForeignKey(d => d.CompaniesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("games_companies_link_ibfk_2");

                entity.HasOne(d => d.Games)
                    .WithMany(p => p.GamesCompaniesLink)
                    .HasForeignKey(d => d.GamesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("games_companies_link_ibfk_1");
            });

            modelBuilder.Entity<GamesGenresLookupLink>(entity =>
            {
                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GamesGenresLookupLink)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("games_genres_lookup_link_ibfk_1");

                entity.HasOne(d => d.GenreLookup)
                    .WithMany(p => p.GamesGenresLookupLink)
                    .HasForeignKey(d => d.GenreLookupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("games_genres_lookup_link_ibfk_2");
            });

            modelBuilder.Entity<GamesPlatformsLink>(entity =>
            {
                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GamesPlatformsLink)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("games_platforms_link_ibfk_1");

                entity.HasOne(d => d.Platform)
                    .WithMany(p => p.GamesPlatformsLink)
                    .HasForeignKey(d => d.PlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("games_platforms_link_ibfk_2");
            });

            modelBuilder.Entity<Artwork>(entity =>
            {
                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Artwork)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("artwork_games_ibfk_1");
            });

            modelBuilder.Entity<Cover>(entity =>
            {
                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Cover)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cover_games_ibfk_1");
            });

            modelBuilder.Entity<PlayRecordComments>(entity =>
            {
                entity.HasOne(d => d.PlayRecord)
                    .WithMany(p => p.PlayRecordComments)
                    .HasForeignKey(d => d.PlayRecordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("play_record_comments_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PlayRecordComments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("play_record_comments_ibfk_1");
            });

            modelBuilder.Entity<PlayRecords>(entity =>
            {
                entity.HasOne(d => d.Game)
                    .WithMany(p => p.PlayRecords)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("play_records_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PlayRecords)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("play_records_ibfk_1");
            });

            modelBuilder.Entity<UserRelationship>(entity =>
            {
                entity.HasOne(d => d.Friend)
                    .WithMany(p => p.UserRelationshipFriend)
                    .HasForeignKey(d => d.FriendId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_relationship_ibfk_2");

                entity.HasOne(d => d.RelationshipTypeLookup)
                    .WithMany(p => p.UserRelationship)
                    .HasForeignKey(d => d.RelationshipTypeLookupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_relationship_ibfk_3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRelationshipUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_relationship_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
