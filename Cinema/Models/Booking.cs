using Microsoft.EntityFrameworkCore;

namespace Cinema.Models
{
    
    public class Booking
    {
     
            public int Id { get; set; }

            public int MovieId { get; set; }
            public Movie Movie { get; set; }

        
            public string ?UserId { get; set; }

            public int SeatsCount { get; set; }
            public double TotalPrice { get; set; }
            public DateTime BookingDate { get; set; } = DateTime.Now;
            public string Status { get; set; } = "Confirmed";
        public string MovieDate {  get; set; }
        public string MovieTime { get; set; }
        
    }
}
