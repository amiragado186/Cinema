using Cinema.Models;

namespace Cinema.Repositories
{
    public interface IPriceRepository
    {
        double Calculate(Movie movie, int seatsCount);
    }
}