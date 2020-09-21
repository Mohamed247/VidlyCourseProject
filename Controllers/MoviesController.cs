using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using VidlyProject.Models;
using VidlyProject.ViewModels;
using System.Data.Entity.Validation;

namespace VidlyProject.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        //can be ViewResult if will return a view
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            //var movies = _context.Movies.Include(c => c.Genre).ToList();

            //return View(movies);

            if (User.IsInRole("CanManageMovies")) 
                return View("List");

            return View("ReadOnlyList");

        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            MovieFormViewModel viewModel = new MovieFormViewModel
            {
                
                Genres = _context.Genres.ToList(),
                Movie = new Movie()
            };
            return View("MoviesForm", viewModel);
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            Movie movie = _context.Movies.ToList().SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            MovieFormViewModel viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MoviesForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(MovieFormViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                MovieFormViewModel viewModel1 = new MovieFormViewModel
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MoviesForm", viewModel1);
            }
            if (viewModel.Movie.ReleaseDate <= DateTime.MinValue)
            {
                viewModel.Movie.ReleaseDate = DateTime.Now;
            }
            if (viewModel.Movie.Id == 0) //create a new movie
            {
                viewModel.Movie.DateAdded = DateTime.Now;
                _context.Movies.Add(viewModel.Movie);
            }
            else
            {
                Movie movieIndDb = _context.Movies.Single(m => m.Id == viewModel.Movie.Id);

                movieIndDb.GenreId = viewModel.Movie.GenreId;
                movieIndDb.Name = viewModel.Movie.Name;
                movieIndDb.ReleaseDate = viewModel.Movie.ReleaseDate;
                movieIndDb.NumberInStock = viewModel.Movie.NumberInStock;

            }
            try
            {
                _context.SaveChanges();
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
            
            
            return RedirectToAction("Index", "Movies");
        }
        public ActionResult Random()
        {
            Movie movie = new Movie() { Name = "Shrek!" };
            //return View(movie);
            //return new EmptyResult();
            //return Content("Hello MVC!");
            //return HttpNotFound();
            //                      Action Controller Args in anonymous obj
            //https://localhost:44396/?arg1=Hi&arg2=damnYou&arg3=1
            //return RedirectToAction("Index", "Home", new { arg1 = "Hi", arg2 = "damnYou", arg3 = 1 });
            List<Customer> customers = new List<Customer>()
            {
                new Customer {Name = "Customer 1"},
                new Customer {Name = "Customer 2"}
            };
            RandomMovieViewModel viewModel = new RandomMovieViewModel()
            {
                Customers = customers,
                Movie = movie
            };

            return View(viewModel);

        }
        //public ActionResult Edit(int id)
        //{
        //    //movies/edit/1      works
        //    //movies/edit?id=1   works
        //    return Content("id = " + id);
        //}
        public ActionResult Edit2(int movieId)
        {
            //movies/edit/1         doesnt work as default route parameter name is id (App_start/RouteConfig)
            //movies/edit?movieid=1 works
            return Content("id = " + movieId);
        }
        

        public ActionResult ByReleaseDate(int year, int month)
        {
            //https://localhost:44396/movies/released/2015/4  embedded paramaters now work because we created a custom route
            //https://localhost:44396/movies/ByReleasedate?year=2015&month=2   works as well
            return Content(year + "/" + month);
        }
        [Route("movies/test/{year:regex(\\d{4})}/{month:range(1,12)}")]  //:regex() to apply regular expresson constraints  :range() to set range of values 
        public ActionResult AttrRoutingTest(int year, int month)
        {
            //https://localhost:44396/movies/test/2000/12  works
            //https://localhost:44396/movies/AttrRoutingTest?year=2015&month=2 doesnt work as route in present above

            return Content(year + "/" + month);
        }

        
        //[Route("Movies/Details/{id}")]

        public ActionResult GetDetails(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).ToList().SingleOrDefault(movieTemp => movieTemp.Id == id);

            if (movie == null) return HttpNotFound();

            return View(movie);
        }
        //movies
        //                        for pageIndex to be optional, must be nullable or can be specified like int pageIndex = 5
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    //https://localhost:44396/movies?pageIndex=2&sortBy=ReleaseDate
        //    //cant embed into the url as it requires a custom route than includes 2 parameters
        //    if (!pageIndex.HasValue) pageIndex = 1;
        //    if (String.IsNullOrWhiteSpace(sortBy)) sortBy = "Name";

        //    return Content(String.Format("pageIndex={0}, & sortBy={1}", pageIndex, sortBy));
        //}

        //public ActionResult ViewMovies()
        //{
        //    var movies = GetMovies();

        //    return View(movies);
        //}

        //private IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie { Id = 1, Name = "Shrek" },
        //        new Movie { Id = 2, Name = "Wall-e" }
        //    };
        //}


    }
}