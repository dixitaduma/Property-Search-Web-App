using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Property_Search_Web_App.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spaces_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Property_Price",
                table: "Properties",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Property_Type",
                table: "Properties",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Space_Size",
                table: "Spaces",
                column: "Size");

            migrationBuilder.CreateIndex(
                name: "IX_Space_Type",
                table: "Spaces",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_PropertyId",
                table: "Spaces",
                column: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spaces");

            migrationBuilder.DropTable(
                name: "Properties");
        }
    }
}
