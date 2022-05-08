using System;

using Domain.Entities;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: nameof(Article),
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Content = table.Column<string>(type: "character varying(32500)", maxLength: 32500, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", item => item.Id);
                });

            migrationBuilder.CreateTable(
                name: nameof(User),
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    LastName = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    UserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", item => item.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleUser",
                columns: table => new
                {
                    ArticleId = table.Column<Guid>(type: "uuid", nullable: false),
                    User_Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleUser", item => new { item.ArticleId, item.User_Id });
                    table.ForeignKey(
                        name: "FK_ArticleUser_Article_Id",
                        column: item => item.ArticleId,
                        principalTable: nameof(Article),
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ArticleUser_User_Id",
                        column: item => item.User_Id,
                        principalTable: nameof(User),
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: nameof(Article));

            migrationBuilder.DropTable(
                name: nameof(User));
        }
    }
}
