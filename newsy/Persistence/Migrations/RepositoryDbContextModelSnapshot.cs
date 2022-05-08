﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence;

namespace Persistence.Migrations
{
    [DbContext(typeof(RepositoryDbContext))]
    partial class RepositoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Domain.Entities.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Content")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(32500)");

                    b.HasKey("Id");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<DateTime>("LastName")
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("UserName")
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Domain.Entities.ArticleUser", b =>
            {
                b.Property<Guid>("ArticleId")
                    .HasColumnType("uuid");

                b.Property<Guid>("User_Id")
                    .HasColumnType("uuid");

                b.HasKey(new string[] { "ArticleId", "User_Id" });

                b.ToTable("ArticleUser");
            });

            modelBuilder.Entity("Domain.Entities.ArticleUser", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithMany("UserArticle")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("UserArticle");
                });

            modelBuilder.Entity("Domain.Entities.ArticleUser", b =>
            {
                b.HasOne("Domain.Entities.Article", null)
                    .WithMany("UserArticle")
                    .HasForeignKey("ArticleId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });

            modelBuilder.Entity("Domain.Entities.Article", b =>
            {
                b.Navigation("UserArticle");
            });
#pragma warning restore 612, 618
        }
    }
}