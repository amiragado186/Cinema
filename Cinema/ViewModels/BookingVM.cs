using System.ComponentModel.DataAnnotations;

namespace Cinema.ViewModels
{
    public class BookingVM
    {
        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public string MovieName { get; set; }
        public string CinemaName { get; set; }
        public string? MainImg { get; set; }
        public double Price { get; set; }
        public int SeatsLeft { get; set; }

        [Range(1, 10, ErrorMessage = "Choose between 1 and 10 seats")]
        public int SeatsCount { get; set; } = 1;
        public string MovieDate { get; set; }
        public string MovieTime { get; set; }
    }
}
