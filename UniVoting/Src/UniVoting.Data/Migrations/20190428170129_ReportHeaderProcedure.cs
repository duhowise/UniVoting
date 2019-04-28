using Microsoft.EntityFrameworkCore.Migrations;

namespace UniVoting.Data.Migrations
{
    public partial class ReportHeaderProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE [dbo].[ReportHeader]
                AS 
                SELECT  Id ,ElectionName ,ElectionSubTitle ,Logo
                ,Colour FROM dbo.ElectionConfigurations 
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE [ReportHeader]");
        }
    }
}
