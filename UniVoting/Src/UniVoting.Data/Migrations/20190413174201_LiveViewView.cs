using Microsoft.EntityFrameworkCore.Migrations;

namespace UniVoting.Data.Migrations
{
    public partial class LiveViewView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW [dbo].[vw_LiveView] 
                AS SELECT DISTINCT COUNT(DISTINCT v.VoterID) AS 'Count',p.PositionName FROM Votes v JOIN Positions p ON v.PositionID = p.ID
                GROUP BY p.PositionName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW [vw_LiveView]");
        }
    }
}
