using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentsApp.Migrations.Module8
{
    public partial class Module8StatusAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketID",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tickets");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tickets",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tickets",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusID",
                table: "Tickets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusID);
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "StatusID", "Name" },
                values: new object[,]
                {
                    { "ToDo", "To Do" },
                    { "InProgress", "In Progress" },
                    { "QA", "Quality Assurance" },
                    { "Done", "Done" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_StatusID",
                table: "Tickets",
                column: "StatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Statuses_StatusID",
                table: "Tickets",
                column: "StatusID",
                principalTable: "Statuses",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Statuses_StatusID",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_StatusID",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "StatusID",
                table: "Tickets");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketID", "Description", "Name", "PointValue", "SprintNumber", "Status" },
                values: new object[] { 1, "This is a test ticket", "Test ticket", 1, 1, "to do" });
        }
    }
}
