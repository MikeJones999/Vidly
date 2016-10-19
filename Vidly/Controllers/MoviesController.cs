using System.Collections.Generic;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
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


    }
}