using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHeroAPI.Migrations
{
    public partial class IntialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamID);
                });

            migrationBuilder.CreateTable(
                name: "SuperHero",
                columns: table => new
                {
                    SuperHeroID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    TeamID = table.Column<int>(type: "int", nullable: false),
                    Debut = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperHero", x => x.SuperHeroID);
                    table.ForeignKey(
                        name: "FK_SuperHero_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Team",
                columns: new[] { "TeamID", "TeamName" },
                values: new object[] { 1, "Justice League" });

            migrationBuilder.InsertData(
                table: "Team",
                columns: new[] { "TeamID", "TeamName" },
                values: new object[] { 2, "Avengers" });

            migrationBuilder.InsertData(
                table: "SuperHero",
                columns: new[] { "SuperHeroID", "Debut", "FirstName", "LastName", "Name", "Place", "TeamID" },
                values: new object[] { 1, (short)1938, "Clark", "Kent", "Superman", "Metropolis", 1 });

            migrationBuilder.InsertData(
                table: "SuperHero",
                columns: new[] { "SuperHeroID", "Debut", "FirstName", "LastName", "Name", "Place", "TeamID" },
                values: new object[] { 2, (short)1963, "Tony", "Stark", "Iron Man", "Malibu", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_SuperHero_TeamID",
                table: "SuperHero",
                column: "TeamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperHero");

            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
