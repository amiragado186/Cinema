using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Migrations
{
    /// <inheritdoc />
    public partial class Add_Category_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Categories (Name) values ('Action and Adventure'); insert into Categories (Name) values ('Drama'); insert into Categories (Name) values ('Horror'); insert into Categories (Name) values ('Science Fiction'); insert into Categories (Name) values ('Fantasy'); insert into Categories (Name) values ('Romance'); insert into Categories (Name) values ('Thriller and Mystery'); insert into Categories (Name) values ('Western'); insert into Categories (Name) values ('Musical'); insert into Categories (Name) values ('Comedy'); ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Categories");
        }
    }
}
