using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiPageAppBeltman.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactID);
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "ContactID", "Address", "Name", "Notes", "Phone" },
                values: new object[] { 1, "642 Evergreen Terrace, Springfield, KY 12345", "Homer Simpson", "Met at Moes", "555-555-5555" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "ContactID", "Address", "Name", "Notes", "Phone" },
                values: new object[] { 2, "123 Fake St, Springfield, KY 12345", "Lenny Leonard", "Stonecutter #12", "555-555-5555" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "ContactID", "Address", "Name", "Notes", "Phone" },
                values: new object[] { 3, "987 Fake St, Springfield, KY 12345", "Carl Carlson", "Stonecutter #14", "555-555-5555" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
