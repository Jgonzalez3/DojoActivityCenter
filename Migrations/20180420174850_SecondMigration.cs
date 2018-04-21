using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BeltExam.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Activities_ActivityId",
                table: "Participants");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "Participants",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_ActivityId",
                table: "Participants",
                newName: "IX_Participants_EventId");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<int>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    duration = table.Column<int>(nullable: false),
                    durationunit = table.Column<string>(nullable: true),
                    time = table.Column<DateTime>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserId",
                table: "Events",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Events_EventId",
                table: "Participants",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Events_EventId",
                table: "Participants");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Participants",
                newName: "ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_EventId",
                table: "Participants",
                newName: "IX_Participants_ActivityId");

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<int>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    duration = table.Column<int>(nullable: false),
                    durationunit = table.Column<string>(nullable: true),
                    time = table.Column<DateTime>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_Activities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_UserId",
                table: "Activities",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Activities_ActivityId",
                table: "Participants",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
