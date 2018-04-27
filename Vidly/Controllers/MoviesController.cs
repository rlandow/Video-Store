using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        private VidlyDataContext _context;

        public MoviesController()
        {
            _context = new VidlyDataContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }

        public ViewResult New()
        {
            
            var genres = _context.Genres.ToList();
            
            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };

            //var movies = _context.Movies.Include(m => m.Genre);

            return View("MovieForm", viewModel);
        }

        public ActionResult Save(Movie movie)
        {
            movie.DateAdded = DateTime.Now;
            _context.Movies.Add(movie);

            _context.SaveChanges();

            var movieList = _context.Movies;

            return View("List", movieList);
        }
        public ActionResult List()
        {
            var movies = _context.Movies.Include(m => m.Genre);
            return View(movies);
        }
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Movie Detail";
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id) ;
            if (movie == null) return HttpNotFound();
            return View(movie); 
        }
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };

            var customers = new List<Customer>
            {
                new Customer { Name = "Customer1"},
                new Customer { Name = "Customer2"}

            };
                    
            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            return Content("id=" + id);
        }

        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue) pageIndex = 1;
            if (string.IsNullOrWhiteSpace(sortBy)) sortBy = "Name";
            return Content(string.Format("Page Index={0}&Sort By = {1}", pageIndex, sortBy));

        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,14)}")]
        public ActionResult ByReleaseYear(int year, int month)
        {
            return Content(year + "/" + month);
        }

        
    }
}