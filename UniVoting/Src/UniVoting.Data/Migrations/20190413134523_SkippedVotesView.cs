using Microsoft.EntityFrameworkCore.Migrations;

namespace UniVoting.Data.Migrations
{
    public partial class SkippedVotesView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW [dbo].[vw_LiveViewSkipped] 
                    AS SELECT DISTINCT COUNT(DISTINCT sv.VoterId) AS 'Count',p.PositionName FROM SkippedVotes sv JOIN Positions p ON sv.PositionID = p.ID
                    GROUP BY p.PositionName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW [vw_LiveViewSkipped]");
        }
    }
}
