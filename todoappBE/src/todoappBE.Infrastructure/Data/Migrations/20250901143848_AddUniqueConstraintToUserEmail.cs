using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace todoappBE.Infrastructure.Data.Migrations;
/// <inheritdoc />
    public partial class AddUniqueConstraintToUserEmail : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.CreateTable(
        name: "Contributors",
        columns: table => new
        {
          Id = table.Column<int>(type: "integer", nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
          Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
          Status = table.Column<int>(type: "integer", nullable: false),
          PhoneNumber_CountryCode = table.Column<string>(type: "text", nullable: true),
          PhoneNumber_Number = table.Column<string>(type: "text", nullable: true),
          PhoneNumber_Extension = table.Column<string>(type: "text", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Contributors", x => x.Id);
        });

    migrationBuilder.CreateTable(
        name: "Users",
        columns: table => new
        {
          Id = table.Column<int>(type: "integer", nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
          Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
          Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
          PasswordHash = table.Column<string>(type: "text", nullable: false),
          CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Users", x => x.Id);
        });

    migrationBuilder.CreateTable(
        name: "Tags",
        columns: table => new
        {
          Id = table.Column<int>(type: "integer", nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
          Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
          UserId = table.Column<int>(type: "integer", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Tags", x => x.Id);
          table.ForeignKey(
                      name: "FK_Tags_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateTable(
        name: "Tasks",
        columns: table => new
        {
          Id = table.Column<int>(type: "integer", nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
          Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
          Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
          CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
          DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
          Status = table.Column<string>(type: "text", nullable: false),
          UserId = table.Column<int>(type: "integer", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Tasks", x => x.Id);
          table.ForeignKey(
                      name: "FK_Tasks_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateTable(
        name: "TaskTag",
        columns: table => new
        {
          TaskId = table.Column<int>(type: "integer", nullable: false),
          TagId = table.Column<int>(type: "integer", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_TaskTag", x => new { x.TaskId, x.TagId });
          table.ForeignKey(
                      name: "FK_TaskTag_Tags_TagId",
                      column: x => x.TagId,
                      principalTable: "Tags",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
                      name: "FK_TaskTag_Tasks_TaskId",
                      column: x => x.TaskId,
                      principalTable: "Tasks",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateIndex(
        name: "IX_Tags_UserId",
        table: "Tags",
        column: "UserId");

    migrationBuilder.CreateIndex(
        name: "IX_Tasks_UserId",
        table: "Tasks",
        column: "UserId");

    migrationBuilder.CreateIndex(
        name: "IX_TaskTag_TagId",
        table: "TaskTag",
        column: "TagId");

    migrationBuilder.CreateIndex(
        name: "IX_Users_Email",
        table: "Users",
        column: "Email",
        unique: true);
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropTable(
        name: "Contributors");

    migrationBuilder.DropTable(
        name: "TaskTag");

    migrationBuilder.DropTable(
        name: "Tags");

    migrationBuilder.DropTable(
        name: "Tasks");

    migrationBuilder.DropTable(
        name: "Users");
  }
}
