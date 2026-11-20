using Cinema.Models;
using Cinema.Repositories;
using Cinema.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Cinema.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class MovieController : Controller
    {
        private readonly IReadRepository<Movie> _movies;
        private readonly IReadRepository<Category> _categories;
        private readonly IReadRepository<Cima> _cinemas;

        public MovieController(
            IReadRepository<Movie> movies,
            IReadRepository<Category> categories,
            IReadRepository<Cima> cinemas)
        {
            _movies = movies;
            _categories = categories;
            _cinemas = cinemas;
        }

        public async Task<IActionResult> Index(string? name, int? categoryId, int? cinemaId, string sort = "recommended")
        {
            Expression<Func<Movie, bool>> filter = m =>
                (string.IsNullOrWhiteSpace(name) || m.Name.Contains(name)) &&
                (!categoryId.HasValue || m.CategoryId == categoryId) &&
                (!cinemaId.HasValue || m.CimaId == cinemaId);

            var movies = await _movies.GetAllAsync(filter);

            IEnumerable<Movie> sortedMovies = sort switch
            {
                "newest" => movies.OrderByDescending(m => m.Id),
                "name" => movies.OrderBy(m => m.Name),
                "price_asc" => movies.OrderBy(m => m.Price),
                "price_desc" => movies.OrderByDescending(m => m.Price),
                _ => movies.OrderByDescending(m => m.Rating)   // recommended
            };

            var vm = new FilterVM
            {
                Name = name,
                CategoryId = categoryId,
                CinemaId = cinemaId,
                Sort = sort,
                Movies = sortedMovies.ToList(),
                Categories = await _categories.GetAllAsync(),
                Cinemas = await _cinemas.GetAllAsync()
            };

            return View(vm);
        }
    }
}