using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DTI.Puzzle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedHistoryChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GlossaryItemId = table.Column<int>(type: "int", nullable: true),
                    ActionId = table.Column<short>(type: "smallint", nullable: true),
                    DateOfChanges = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryChanges_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HistoryChanges_GlossaryItems_GlossaryItemId",
                        column: x => x.GlossaryItemId,
                        principalTable: "GlossaryItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryChanges_ActionId",
                table: "HistoryChanges",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryChanges_GlossaryItemId",
                table: "HistoryChanges",
                column: "GlossaryItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryChanges");
        }
    }
}
