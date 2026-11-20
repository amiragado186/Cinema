using Cinema.Models;

namespace Cinema.Repositories
{
    public class PriceRepository : IPriceRepository
    {
        public double Calculate(Movie movie, int seatsCount)
        {
            return (movie.Price ?? 0f) * seatsCount;
        }
    }
}