using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bowling_Centre_Easy.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMemberInheritance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberInfoMemberID",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BaseMember",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GamesWon = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseMember", x => x.MemberID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_MemberInfoMemberID",
                table: "Players",
                column: "MemberInfoMemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_BaseMember_MemberInfoMemberID",
                table: "Players",
                column: "MemberInfoMemberID",
                principalTable: "BaseMember",
                principalColumn: "MemberID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_BaseMember_MemberInfoMemberID",
                table: "Players");

            migrationBuilder.DropTable(
                name: "BaseMember");

            migrationBuilder.DropIndex(
                name: "IX_Players_MemberInfoMemberID",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "MemberInfoMemberID",
                table: "Players");
        }
    }
}
