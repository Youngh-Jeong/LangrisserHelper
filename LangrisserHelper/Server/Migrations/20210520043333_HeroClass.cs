using Microsoft.EntityFrameworkCore.Migrations;

namespace LangrisserHelper.Server.Migrations
{
    public partial class HeroClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeroClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HeroId = table.Column<int>(nullable: false),
                    ArmyBranchId = table.Column<int>(nullable: false),
                    HeroClassName = table.Column<string>(nullable: true),
                    IsSP = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeroClasses_ArmyBranches_ArmyBranchId",
                        column: x => x.ArmyBranchId,
                        principalTable: "ArmyBranches",
                        principalColumn: "ArmyBranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroClasses_Heros_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heros",
                        principalColumn: "HeroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeroClasses_ArmyBranchId",
                table: "HeroClasses",
                column: "ArmyBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroClasses_HeroId",
                table: "HeroClasses",
                column: "HeroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeroClasses");
        }
    }
}
