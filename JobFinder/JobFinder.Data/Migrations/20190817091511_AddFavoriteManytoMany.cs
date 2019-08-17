using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Data.Migrations
{
    public partial class AddFavoriteManytoMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "FavoriteUserJobAds",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    JobAddId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteUserJobAds", x => new { x.JobAddId, x.UserId });
                    table.ForeignKey(
                        name: "FK_FavoriteUserJobAds_JobAdds_JobAddId",
                        column: x => x.JobAddId,
                        principalTable: "JobAdds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteUserJobAds_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteUserJobAds_UserId",
                table: "FavoriteUserJobAds",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteUserJobAds");

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
    }
}
