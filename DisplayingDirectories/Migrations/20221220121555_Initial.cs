using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisplayingDirectories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Folders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FolderType = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Folders_Folders_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Folders",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Id", "FolderType", "Name", "ParentId" },
                values: new object[] { 1, "Directory", "Creating Digital Images", null });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Id", "FolderType", "Name", "ParentId" },
                values: new object[] { 2, "Directory", "Resources", 1 });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Id", "FolderType", "Name", "ParentId" },
                values: new object[] { 3, "Directory", "Evidence", 1 });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Id", "FolderType", "Name", "ParentId" },
                values: new object[] { 4, "Directory", "Graphic Products", 1 });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Id", "FolderType", "Name", "ParentId" },
                values: new object[,]
                {
                    { 5, "Directory", "Primary Sources", 2 },
                    { 6, "Directory", "Secondary Sources", 2 },
                    { 7, "Directory", "Process", 4 },
                    { 8, "Directory", "Final Product", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Folders_ParentId",
                table: "Folders",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Folders");
        }
    }
}
