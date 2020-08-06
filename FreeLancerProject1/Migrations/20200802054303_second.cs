using Microsoft.EntityFrameworkCore.Migrations;

namespace FreeLancerProject1.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string proccedure = @"CREATE PROCEDURE Test
                                    @Id int
                                    AS
                                    Begin
	                                    SELECT * from Employee where Id=@Id;
                                    End";
            migrationBuilder.Sql(proccedure);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
