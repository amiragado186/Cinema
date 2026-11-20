using Cinema.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [MinMaxLengthCustom]
        public string Name { get; set; }

        public int? Rating { get; set; }
        public string? Description { get; set; }
        public float? Price { get; set; }
        public bool? Status { get; set; }
        public DateOnly? Date { get; set; }
        public TimeOnly? Time { get; set; }

        public string? MainImg { get; set; }                       // attribute removed
        public List<MovieSubImg>? SubImgs { get; set; } = new();   // attribute removed
        public List<Actor>? Actors { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }                    // now nullable
        public int? CimaId { get; set; }
        public Cima? Cima { get; set; }                            // now nullable
    }
}
