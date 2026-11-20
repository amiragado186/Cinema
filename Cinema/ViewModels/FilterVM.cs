using Cinema.Models;

namespace Cinema.ViewModels
{
    public class FilterVM
    {
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public int? CinemaId { get; set; }
        public string Sort { get; set; } = "recommended";

        public IEnumerable<Movie> Movies { get; set; } = new List<Movie>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<Cima> Cinemas { get; set; } = new List<Cima>();
    }
}
