using Microsoft.EntityFrameworkCore.Migrations;

namespace LangrisserHelper.Server.Migrations
{
    public partial class ArmyBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArmyBranches",
                columns: table => new
                {
                    ArmyBranchId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArmyName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArmyBranches", x => x.ArmyBranchId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArmyBranches");
        }
    }
}
