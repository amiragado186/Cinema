using Cinema.Models;
using Cinema.Repositories;
using Cinema.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cinema.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IReadRepository<Movie> _movies;

        public HomeController(IReadRepository<Movie> movies)
        {
            _movies = movies;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movies.GetAllAsync();
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}