using Cinema.Models;
using Cinema.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace Cinema.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BookingController : Controller
    {
        private readonly IRepository<Booking> _bookings;
        private readonly IReadRepository<Movie> _movies;
        private readonly IPriceRepository _priceCalculator;

        public BookingController(
            IRepository<Booking> bookings,
            IReadRepository<Movie> movies,
            IPriceRepository priceCalculator)
        {
            _bookings = bookings;
            _movies = movies;
            _priceCalculator = priceCalculator;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _bookings.GetAllAsync(null, false, b => b.Movie);

            return View(bookings.OrderByDescending(b => b.BookingDate));
        }

        public async Task<IActionResult> Details(int id)
        {
            var booking = await _bookings.GetOneAsync(b => b.Id == id, false, b => b.Movie);

            if (booking == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            return View(booking);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int movieId)
        {
            var movie = await _movies.GetOneAsync(m => m.Id == movieId, tracked: false);
            if (movie == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            var booking = new Booking
            {
                MovieId = movie.Id,
                Movie = movie,
                SeatsCount = 1
            };

            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int movieId, int seatsCount, string movieDate, string movieTime)
        {
            var movie = await _movies.GetOneAsync(m => m.Id == movieId, tracked: false);
            if (movie == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            if (seatsCount < 1)
                ModelState.AddModelError(nameof(Booking.SeatsCount), "Please select at least one seat.");

            if (string.IsNullOrWhiteSpace(movieDate))
                ModelState.AddModelError(nameof(Booking.MovieDate), "Please choose a date.");

            if (string.IsNullOrWhiteSpace(movieTime))
                ModelState.AddModelError(nameof(Booking.MovieTime), "Please choose a time.");

            if (!ModelState.IsValid)
            {
                return View(new Booking
                {
                    MovieId = movieId,
                    Movie = movie,
                    SeatsCount = seatsCount,
                    MovieDate = movieDate,
                    MovieTime = movieTime
                });
            }

            var booking = new Booking
            {
                MovieId = movieId,
                SeatsCount = seatsCount,
                // calculated on the server so the price can't be changed from the form
                TotalPrice = _priceCalculator.Calculate(movie, seatsCount),
                BookingDate = DateTime.Now,
                Status = "Confirmed",
                MovieDate = movieDate,
                MovieTime = movieTime
            };

            _bookings.Add(booking);
            await _bookings.CommitAsync();

            return RedirectToAction("Details", new { id = booking.Id });
        }

        public async Task<IActionResult> Cancel(int id)
        {
            var booking = await _bookings.GetOneAsync(b => b.Id == id);
            if (booking == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            // keep the record, only change the status
            booking.Status = "Cancelled";
            await _bookings.CommitAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _bookings.GetOneAsync(b => b.Id == id);
            if (booking == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            _bookings.Delete(booking);
            await _bookings.CommitAsync();

            return RedirectToAction("Index");
        }
    }
}