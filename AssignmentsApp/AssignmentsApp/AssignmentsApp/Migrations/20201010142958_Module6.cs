using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentsApp.Migrations
{
    public partial class Module6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbr = table.Column<string>(nullable: true),
                    Game = table.Column<string>(nullable: true),
                    Sport = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "Abbr", "Category", "Game", "Name", "Sport" },
                values: new object[,]
                {
                    { 1, "CA", "Indoor", "Winter Olympics", "Canada", "Curling" },
                    { 22, "FI", "Outdoor", "Youth Olympic Games", "Finland", "Skateboarding" },
                    { 21, "RU", "Indoor", "Youth Olympic Games", "Russia", "Breakdancing" },
                    { 20, "CY", "Indoor", "Youth Olympic Games", "Cyprus", "Breakdancing" },
                    { 19, "FR", "Indoor", "Youth Olympic Games", "France", "Breakdancing" },
                    { 18, "ZW", "Outdoor", "Paralympics", "Zimbabwe", "Canoe Sprint" },
                    { 17, "PK", "Outdoor", "Paralympics", "Pakistan", "Canoe Sprint" },
                    { 16, "AT", "Outdoor", "Paralympics", "Austria", "Canoe Sprint" },
                    { 15, "UA", "Indoor", "Paralympics", "Ukraine", "Archery" },
                    { 14, "UY", "Indoor", "Paralympics", "Uruguary", "Archery" },
                    { 13, "TH", "Indoor", "Paralympics", "Thailand", "Archery" },
                    { 12, "US", "Outdoor", "Summer Olympics", "United States", "Road Cycling" },
                    { 11, "NL", "Outdoor", "Summer Olympics", "Netherlands", "Road Cycling" },
                    { 10, "BR", "Outdoor", "Summer Olympics", "Brazil", "Road Cycling" },
                    { 9, "MX", "Indoor", "Summer Olympics", "Mexico", "Diving" },
                    { 8, "CN", "Indoor", "Summer Olympics", "China", "Diving" },
                    { 7, "DE", "Indoor", "Summer Olympics", "Germany", "Diving" },
                    { 6, "JP", "Outdoor", "Winter Olympics", "Japan", "Bobsleigh" },
                    { 5, "IT", "Outdoor", "Winter Olympics", "Italy", "Bobsleigh" },
                    { 4, "JM", "Outdoor", "Winter Olympics", "Jamaica", "Bobsleigh" },
                    { 3, "GB", "Indoor", "Winter Olympics", "Great Britain", "Curling" },
                    { 2, "SE", "Indoor", "Winter Olympics", "Sweden", "Curling" },
                    { 23, "SK", "Outdoor", "Youth Olympic Games", "Slovakia", "Skateboarding" },
                    { 24, "PT", "Outdoor", "Youth Olympic Games", "Portugal", "Skateboarding" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
