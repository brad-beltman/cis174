using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class Addedheadingscolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Headings",
                table: "Reports",
                nullable: true);

            migrationBuilder.InsertData(
                table: "ReportTypes",
                columns: new[] { "ReportTypeID", "Name" },
                values: new object[,]
                {
                    { 7, "RedTeam" },
                    { 8, "Phishing" },
                    { 9, "VA" },
                    { 10, "Other" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "ReportTypeID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "ReportTypeID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "ReportTypeID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "ReportTypeID",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "Headings",
                table: "Reports");
        }
    }
}
