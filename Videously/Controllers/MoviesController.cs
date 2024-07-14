using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Videously.Models;
using Videously.ViewModels;

namespace Videously.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
        // GET: Movies/Random
        //public ActionResult Random()
        //{
        //    // either Model can be created and passed to View
        //    // or make use of ViewDataDictionary in the controller
        //    // or make use of ViewBag

        //    //check the view for corresponding approaches

        //    #region Approach - 1
        //    //var movie = new Movie { Title = "Shrek!" };
        //    //return View(movie);
        //    #endregion

        //    #region Approach - 2
        //    //var movie1 = new Movie { Title = "Lone Ranger!" };
        //    //ViewData["Movie"] = movie1;
        //    //return View();
        //    #endregion

        //    #region Approach - 3
        //    //var movie2 = new Movie { Title = "Hello" };
        //    //ViewBag.Movie = movie2;
        //    //return View();
        //    #endregion

        //    //First approach is proper and efficient to use

        //    var movie = new Movie { Title = "Shrek!" };
        //    var customers = new List<Customer>
        //    {
        //        new Customer{Name="A"},
        //        new Customer{Name="B"},
        //        new Customer{Name="C"},
        //    };
        //    var randomMovieViewModel = new RandomMovieViewModel { Movie = movie, Customers = customers };
        //    return View(randomMovieViewModel);

        //    #region different ActionResult in MVC
        //    /*
        //    return Content("Hello World!");
        //    return HttpNotFound();
        //    return new EmptyResult();
        //    return RedirectToAction("Index", "Home", new { Page = 1, SortBy = "name" });
        //    */
        //    #endregion
        //}

        public ActionResult Edit(int id)
        {
            return Content($"id: {id}");
        }

        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    pageIndex = pageIndex ?? 1;
        //    if (string.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";
        //    return Content($"pageIndex:{pageIndex} & sortBy:{sortBy}");
        //}

        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content($"{year}/{month}");
        }

        //[Route("movies")]
        public ViewResult Index()
        {
            var movies = GetMovies();
            return View(movies);
        }

        private List<Movie> GetMovies()
        {
            return _context.Movies.Include(m => m.Genre).ToList();
            //return new List<Movie>
            //{
            //    new Movie{Id=1,Title="Sherk"},
            //    new Movie{Id=2,Title="Lone Ranger"}
            //};
        }

        public ActionResult Details(int id)
        {
            try
            {
                var movie = GetMovies()[id - 1];
                if (movie == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(movie);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return HttpNotFound($"Customer does exist with index: {id}");
            }
        }

        public ActionResult AddMovie()
        {
            var genres = _context.Genres.ToList();
            var movieFormViewModel = new MovieFormViewModel
            {
                Genres = genres,
            };
            return View("MovieForm", movieFormViewModel);
        }

        public ActionResult EditMovie(int id)
        {
            try
            {
                var movie = GetMovies()[id - 1];
                if (movie == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    var movieForm = new MovieFormViewModel
                    {
                        Movie = movie,
                        Genres = _context.Genres.ToList(),
                    };
                    return View("MovieForm", movieForm);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return HttpNotFound($"Customer does exist with index: {id}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveMovie(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var movieForm = new MovieFormViewModel
                {
                    Genres = _context.Genres.ToList(),
                    Movie = new Movie()
                };
                return View("MovieForm", movieForm);
            }
            MapMissingDetailsToMovie(movie);
            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = GetMovies()[movie.Id - 1];

                Mapper.Map<Movie, Movie>(movie, movieInDb); //can be used MovieDto. Dto -> Data Transfer Object
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        private void MapMissingDetailsToMovie(Movie movie)
        {
            movie.Genre = _context.Genres.SingleOrDefault(m => m.Id == movie.Genre.Id);
            movie.DateAdded = DateTime.Now;
        }
    }
}