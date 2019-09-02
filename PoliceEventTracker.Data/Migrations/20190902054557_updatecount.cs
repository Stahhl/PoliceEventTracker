using Microsoft.EntityFrameworkCore.Migrations;

namespace PoliceEventTracker.Data.Migrations
{
    public partial class updatecount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Updates",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Updates");
        }
    }
}
