using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniVoting.Data.Migrations
{
    public partial class fac : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkippedVoteses");

            migrationBuilder.DropColumn(
                name: "Faculty",
                table: "Voters");

            migrationBuilder.DropColumn(
                name: "Faculty",
                table: "Positions");

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Voters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Positions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FacultyName = table.Column<string>(unicode: false, maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkippedVotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Positionid = table.Column<int>(nullable: false),
                    VoterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkippedVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkippedVotes_Positions_Positionid",
                        column: x => x.Positionid,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkippedVotes_Voters_VoterId",
                        column: x => x.VoterId,
                        principalTable: "Voters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Voters_FacultyId",
                table: "Voters",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_FacultyId",
                table: "Positions",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_SkippedVotes_Positionid",
                table: "SkippedVotes",
                column: "Positionid");

            migrationBuilder.CreateIndex(
                name: "IX_SkippedVotes_VoterId",
                table: "SkippedVotes",
                column: "VoterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Faculties_FacultyId",
                table: "Positions",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Voters_Faculties_FacultyId",
                table: "Voters",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Faculties_FacultyId",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_Voters_Faculties_FacultyId",
                table: "Voters");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "SkippedVotes");

            migrationBuilder.DropIndex(
                name: "IX_Voters_FacultyId",
                table: "Voters");

            migrationBuilder.DropIndex(
                name: "IX_Positions_FacultyId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Voters");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Positions");

            migrationBuilder.AddColumn<string>(
                name: "Faculty",
                table: "Voters",
                unicode: false,
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Faculty",
                table: "Positions",
                unicode: false,
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SkippedVoteses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Positionid = table.Column<int>(nullable: false),
                    VoterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkippedVoteses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkippedVoteses_Positions_Positionid",
                        column: x => x.Positionid,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkippedVoteses_Voters_VoterId",
                        column: x => x.VoterId,
                        principalTable: "Voters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkippedVoteses_Positionid",
                table: "SkippedVoteses",
                column: "Positionid");

            migrationBuilder.CreateIndex(
                name: "IX_SkippedVoteses_VoterId",
                table: "SkippedVoteses",
                column: "VoterId");
        }
    }
}
