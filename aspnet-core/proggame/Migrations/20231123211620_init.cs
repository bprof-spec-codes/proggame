using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proggame.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AppTasks",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSolutions",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSolutions_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppSolutions_AppTasks_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "dbo",
                        principalTable: "AppTasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppTests",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTests_AppTasks_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "dbo",
                        principalTable: "AppTasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSolutions_CreatorId",
                schema: "dbo",
                table: "AppSolutions",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSolutions_TaskId",
                schema: "dbo",
                table: "AppSolutions",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTests_TaskId",
                schema: "dbo",
                table: "AppTests",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSolutions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AppTests",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AppTasks",
                schema: "dbo");
        }
    }
}
