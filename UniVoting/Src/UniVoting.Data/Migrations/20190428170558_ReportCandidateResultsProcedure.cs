using Microsoft.EntityFrameworkCore.Migrations;

namespace UniVoting.Data.Migrations
{
    public partial class ReportCandidateResultsProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE [dbo].[ReportCandidateResults]
                AS 
                SELECT  vw_CandidateVotes.Name, c.CandidatePicture, vw_CandidateVotes.Position,
                vw_CandidateVotes.Vote, vw_CandidateVotes.Percentage AS 'Percentage Votes'
                   FROM vw_CandidateVotes INNER JOIN
                    Candidates AS c ON vw_CandidateVotes.ID = c.ID
                ORDER BY c.PositionID
                ");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE [ReportCandidateResults]");
        }
    }
}
