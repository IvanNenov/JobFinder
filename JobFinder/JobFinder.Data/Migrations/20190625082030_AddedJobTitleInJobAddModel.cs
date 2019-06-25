using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Data.Migrations
{
    public partial class AddedJobTitleInJobAddModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "JobAdds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "JobAdds");
        }
    }
}
