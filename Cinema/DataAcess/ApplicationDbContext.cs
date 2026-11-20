using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.DataAcess
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Cinema;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cima>Cimas { get; set; }
        public DbSet<Movie>Movies { get; set; }
        public DbSet<MovieSubImg> MovieSubImgs { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Booking> Bookings { get; set; }
       
    }

}
