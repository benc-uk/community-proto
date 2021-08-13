using Microsoft.EntityFrameworkCore.Migrations;

namespace CommunityApi.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_Communities_CommunityId",
                table: "Discussions");

            migrationBuilder.RenameColumn(
                name: "CommunityId",
                table: "Discussions",
                newName: "CommunityId1");

            migrationBuilder.RenameIndex(
                name: "IX_Discussions_CommunityId",
                table: "Discussions",
                newName: "IX_Discussions_CommunityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_Communities_CommunityId1",
                table: "Discussions",
                column: "CommunityId1",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_Communities_CommunityId1",
                table: "Discussions");

            migrationBuilder.RenameColumn(
                name: "CommunityId1",
                table: "Discussions",
                newName: "CommunityId");

            migrationBuilder.RenameIndex(
                name: "IX_Discussions_CommunityId1",
                table: "Discussions",
                newName: "IX_Discussions_CommunityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_Communities_CommunityId",
                table: "Discussions",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
