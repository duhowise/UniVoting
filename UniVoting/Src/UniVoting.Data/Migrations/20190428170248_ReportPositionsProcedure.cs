using Microsoft.EntityFrameworkCore.Migrations;

namespace UniVoting.Data.Migrations
{
    public partial class ReportPositionsProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE [dbo].[ReportPositions]
                AS 
                SELECT PositionName FROM dbo.Positions
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE [ReportPositions]");
        }
    }
}
