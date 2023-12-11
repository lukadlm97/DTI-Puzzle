using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DTI.Puzzle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Drop_Table_Dictionary_called : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DictionaryChanges");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DictionaryChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionId = table.Column<short>(type: "smallint", nullable: true),
                    GlossaryItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DictionaryChanges_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DictionaryChanges_GlossaryItems_GlossaryItemId",
                        column: x => x.GlossaryItemId,
                        principalTable: "GlossaryItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryChanges_ActionId",
                table: "DictionaryChanges",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryChanges_GlossaryItemId",
                table: "DictionaryChanges",
                column: "GlossaryItemId");
        }
    }
}
