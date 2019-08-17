using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Data.Migrations
{
    public partial class addFavouriteJobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "JobAdds",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobAdds_UserId",
                table: "JobAdds",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdds_AspNetUsers_UserId",
                table: "JobAdds",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAdds_AspNetUsers_UserId",
                table: "JobAdds");

            migrationBuilder.DropIndex(
                name: "IX_JobAdds_UserId",
                table: "JobAdds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "JobAdds");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
