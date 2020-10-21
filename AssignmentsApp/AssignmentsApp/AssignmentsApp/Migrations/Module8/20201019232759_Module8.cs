using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentsApp.Migrations.Module8
{
    public partial class Module8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SprintNumber = table.Column<int>(nullable: false),
                    PointValue = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketID);
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketID", "Description", "Name", "PointValue", "SprintNumber", "Status" },
                values: new object[] { 1, "This is a test ticket", "Test ticket", 1, 1, "to do" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
