using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Migrations
{
    /// <inheritdoc />
    public partial class add_data_toCimas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Cimas (Name, Img) values ('Cinéma Studio 28', 'Cinéma Studio 28'); insert into Cimas (Name, Img) values ('Plaza Theatre', 'Plaza Theatre'); insert into Cimas (Name, Img) values ('Americana Plaza', 'Americana Plaza'); insert into Cimas (Name, Img) values ('Curzon Bloomsbury', 'Curzon Bloomsbury'); insert into Cimas (Name, Img) values ('Grand Rex', 'Grand Rex'); insert into Cimas (Name, Img) values ('New Beverly Cinema', 'New Beverly Cinema'); ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Cimas");
        }
    }
}
