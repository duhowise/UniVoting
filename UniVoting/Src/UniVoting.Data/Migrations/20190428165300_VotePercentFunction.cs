using Microsoft.EntityFrameworkCore.Migrations;

namespace UniVoting.Data.Migrations
{
    public partial class VotePercentFunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE FUNCTION [dbo].[VotePercent](@param1 FLOAT,@param2 FLOAT)
                                    RETURNS DECIMAL(18,2)
                AS BEGIN
                  DECLARE @result FLOAT
                    SET @result=0
                    IF @param2=0 BEGIN  SET @result=((@param1/@param1)*100.00)	
                   END
                    ELSE
                      BEGIN
      	                SET @result=((@param1/@param2)*100)
                      END
	                RETURN @result
                END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP FUNCTION [VotePercent]");
        }
    }
}
