using Microsoft.EntityFrameworkCore.Migrations;

namespace UniVoting.Data.Migrations
{
    public partial class VoteCountFunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE FUNCTION [dbo].[VOteCount](@position NVARCHAR(50))
                RETURNS INT
                AS BEGIN
                  DECLARE @count INT
                    SET @count=(ISNULL((SELECT [Count] FROM vw_LiveView vlv  WHERE vlv.PositionName=@position),0))
	                RETURN @count
                END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP FUNCTION [VOteCount]");
        }
    }
}
