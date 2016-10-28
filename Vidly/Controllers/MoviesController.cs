using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
namespace Vidly.Controllers
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
         }


    // GET: Movies
    public ActionResult Random()
        {
            var movie = new Movie { Name = "Shrek" };

            //return View(movie);

            ////[[return just text]] ////
            //return Content("Hello world");

            ////[[return Page not found]]////
            //return HttpNotFound("Broken");

            ////[[Empty no results]]////
            //return new EmptyResult();

            ///[[Redirect to another page]]/////
            ///param 1 = "method", param 2 = "Controller"  - returns to home page and the url doesnt show
            //return RedirectToAction("Index", "Home");

            ///[[Redirect to another page]]/////
            ///param 1 = "method", param 2 = "Controller"  - returns to home page and the url doesnt show 
            ///however - passing params does shows up in url
            //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });



            var customers = new List<Customer>
            {
                new Customer {Name= "Customer 1" },
                new Customer {Name = "Customer 2" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };


            return View(viewModel);

        }


        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        //Localhost:16608/movies/released/2016/04
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }



        public ActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }


        public ActionResult Details(int Id)
        {
            var movie = _context.Movies.Include(mbox => mbox.Genre).SingleOrDefault(c => c.Id == Id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }


        public ActionResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                Movie = new Movie(),
                Genres = genres

            };

            return View("MovieForm", viewModel);

        }

        public ActionResult Edit(int id)
        {

            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {


            //if the form is incomplete or data not valid (as per data annotations in Customer.cs) - resend them to list of customers
            //This is especially necessary for non standard validation checks 
            if (!ModelState.IsValid)
            {

                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = _context.Genres.ToList()

                };

                //Debug.WriteLine("***DEBUG*** Invalid completion of form");
                return View("MovieForm", viewModel);
            }


            if (movie.Id == 0)
            {
                //add new movie to database
                _context.Movies.Add(movie);
            }
            else
            {
                //get the link to the movie from DB
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.Genre = movie.Genre;
                movieInDb.NumberInStock = movie.NumberInStock;

            }

            _context.SaveChanges();


            return RedirectToAction("Index", "Movies");

        }



    }
}