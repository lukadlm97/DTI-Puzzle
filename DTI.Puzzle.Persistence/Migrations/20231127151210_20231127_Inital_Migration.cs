using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DTI.Puzzle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _20231127_Inital_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlossaryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Term = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Definition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlossaryItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DictionaryChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GlossaryItemId = table.Column<int>(type: "int", nullable: true),
                    ActionId = table.Column<short>(type: "smallint", nullable: true)
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

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (short)1, "Create" },
                    { (short)2, "Update" },
                    { (short)3, "Delete" }
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DictionaryChanges");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "GlossaryItems");
        }
    }
}
