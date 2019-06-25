using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Data.Migrations
{
    public partial class AddedJobAddLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "JobAdds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "JobAdds");
        }
    }
}
