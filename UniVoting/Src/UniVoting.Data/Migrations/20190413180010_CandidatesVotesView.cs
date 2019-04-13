using Microsoft.EntityFrameworkCore.Migrations;

namespace UniVoting.Data.Migrations
{
    public partial class CandidatesVotesView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW [dbo].[vw_CandidateVotes] 
                AS SELECT
                  c.ID
                 ,c.CandidateName AS 'Name'
                 ,p.PositionName AS 'Position'
                 ,dbo.VOteCount(p.PositionName) AS 'Vote',dbo.VoteSkipped(p.PositionName) AS 'Skipped',
				 dbo.VotePercent(COUNT(DISTINCT v.VoterID),
				 dbo.VOteCount(p.PositionName) + dbo.VoteSkipped(p.PositionName)) AS 'Percentage'
                 ,dbo.VotePercent(dbo.VoteSkipped(p.PositionName), dbo.VOteCount(p.PositionName) +
				 dbo.VoteSkipped(p.PositionName)) AS 'Skipped Percentage'

                FROM dbo.Candidates c LEFT OUTER JOIN dbo.Votes v
                  ON c.ID = v.CandidateID LEFT OUTER JOIN dbo.Positions p
                  ON c.PositionID = p.ID  GROUP BY c.CandidateName
                        ,p.PositionName,c.ID,dbo.VoteSkipped(p.PositionName)");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW [vw_CandidateVotes]");
        }
    }
}
