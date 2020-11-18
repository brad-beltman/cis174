using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class AddedFULLTEXTindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                sql: "CREATE FULLTEXT CATALOG ftCatalog AS DEFAULT;",
                suppressTransaction: true);

            migrationBuilder.Sql(
                sql: "CREATE FULLTEXT INDEX ON Posts(SearchIndex) KEY INDEX PK_Posts;",
                suppressTransaction: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
