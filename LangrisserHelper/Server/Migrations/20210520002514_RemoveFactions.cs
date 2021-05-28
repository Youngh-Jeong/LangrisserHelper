using Microsoft.EntityFrameworkCore.Migrations;

namespace LangrisserHelper.Server.Migrations
{
    public partial class RemoveFactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TABLE Factionas", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
