using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class GetAllPersons_SToredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_GetAllPersons = @"Create Procedure [dbo].[GetAllPersons]
             As Begin
             SELECT PersonID, PersonName, Email, DateOfBirth, Gender, CountryID, Address, ReceiveNewsLetters FROM [dbo].[Persons]
             End
         ";
            migrationBuilder.Sql(sp_GetAllPersons);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_GetAllPersons = @"
        DROP PROCEDURE [dbo].[GetAllPersons]
      ";
            migrationBuilder.Sql(sp_GetAllPersons);
        }
    }
}
