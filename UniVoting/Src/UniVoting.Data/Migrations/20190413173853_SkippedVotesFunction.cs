using Microsoft.EntityFrameworkCore.Migrations;

namespace UniVoting.Data.Migrations
{
    public partial class SkippedVotesFunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE FUNCTION [dbo].[VoteSkipped](@position NVARCHAR(50))
                RETURNS INT
                AS BEGIN
                  DECLARE @count INT
                    SET @count=(ISNULL((SELECT [Count] FROM vw_LiveViewSkipped vlvs WHERE vlvs.PositionName=@position),0))
	                RETURN @count
                END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP FUNCTION [VoteSkipped]");
        }
    }
}
