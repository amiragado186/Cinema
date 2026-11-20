using Cinema.Models;
using Cinema.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace Cinema.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class MovieController : Controller
    {
        private readonly IRepository<Movie> _movies;
        private readonly IReadRepository<Actor> _actors;
        private readonly IRepository<MovieSubImg> _subImgs;
        private readonly IImageRepository _images;

        public MovieController(
            IRepository<Movie> movies,
            IReadRepository<Actor> actors,
            IRepository<MovieSubImg> subImgs,
            IImageRepository images)
        {
            _movies = movies;
            _actors = actors;
            _subImgs = subImgs;
            _images = images;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _movies.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Actors = await _actors.GetAllAsync();
            return View(new Movie());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movie movie, IFormFile? ImageFile, List<IFormFile>? SubImgFiles, List<int>? ActorIds)
        {
            ModelState.Remove(nameof(Movie.MainImg));
            ModelState.Remove(nameof(Movie.SubImgs));
            ModelState.Remove(nameof(Movie.Actors));
            ModelState.Remove(nameof(Movie.Category));
            ModelState.Remove(nameof(Movie.Cima));

            if (ImageFile is null || ImageFile.Length == 0)
                ModelState.AddModelError("MainImg", "Please Enter the Images Again");
            if (!ModelState.IsValid)
            {
                ViewBag.Actors = await _actors.GetAllAsync();
                ViewBag.SelectedActorIds = ActorIds ?? new List<int>();
                TempData["Error_Notificatioin"] = "Invalid Data";
                return View(movie);
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                movie.MainImg = await _images.SaveAsync(ImageFile);
            }

            movie.SubImgs = new List<MovieSubImg>();
            if (SubImgFiles != null)
            {
                foreach (var file in SubImgFiles)
                {
                    if (file != null && file.Length > 0)
                    {
                        movie.SubImgs.Add(new MovieSubImg { Img = await _images.SaveAsync(file) });
                    }
                }
            }

            movie.Actors = ActorIds != null
                ? await _actors.GetAllAsync(a => ActorIds.Contains(a.Id), tracked: true)
                : new List<Actor>();

            _movies.Add(movie);
            await _movies.CommitAsync();
            TempData["Successful_Notification"] = "The Movie is created succesfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var movie = await _movies.GetOneAsync(x => x.Id == id, true, x => x.SubImgs);
            if (movie == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Movie movie, IFormFile? ImageFile, List<IFormFile>? SubImgs, List<int>? RemoveSubImgIds)
        {
            var movieInDB = await _movies.GetOneAsync(x => x.Id == movie.Id, tracked: false);
            if (movieInDB == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                movie.MainImg = await _images.SaveAsync(ImageFile);
                _images.Delete(movieInDB.MainImg);
            }
            else
            {
                movie.MainImg = movieInDB.MainImg;
            }

            if (RemoveSubImgIds != null && RemoveSubImgIds.Count > 0)
            {
                var toRemove = await _subImgs.GetAllAsync(
                    x => x.MovieId == movie.Id && RemoveSubImgIds.Contains(x.Id),
                    tracked: true);

                foreach (var sub in toRemove)
                {
                    _images.Delete(sub.Img);
                }
                _subImgs.DeleteRange(toRemove);
            }

            if (SubImgs != null)
            {
                foreach (var file in SubImgs)
                {
                    if (file != null && file.Length > 0)
                    {
                        _subImgs.Add(new MovieSubImg
                        {
                            MovieId = movie.Id,
                            Img = await _images.SaveAsync(file)
                        });
                    }
                }
            }

            _movies.Update(movie);
            await _movies.CommitAsync();
            TempData["Update_Notification"] = "The Movie is Edited succesfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movies.GetOneAsync(x => x.Id == id, true, x => x.SubImgs);
            if (movie == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            _images.Delete(movie.MainImg);
            foreach (var sub in movie.SubImgs)
            {
                _images.Delete(sub.Img);
            }

            _movies.Delete(movie);
            await _movies.CommitAsync();
            TempData["Delete_Notification"] = "The Movie is Deleted ";
            return RedirectToAction("Index");
        }
    }
}