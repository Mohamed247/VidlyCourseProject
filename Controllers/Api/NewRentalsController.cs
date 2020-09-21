using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VidlyProject.Models;
using VidlyProject.Dtos;
using AutoMapper;

namespace VidlyProject.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }
       
        [HttpGet]
        public IHttpActionResult GetCustomers()
        {
            return Ok(_context.MovieRentals
                .ToList()
                );
        }
        
        [HttpPost]
        public IHttpActionResult CreateNewMovieRentals(NewRentalDto newRental)
        {

            if (newRental.MovieIds.Count == 0)
                return BadRequest("No Movie Ids have been given");

            Customer customer = _context.Customers.SingleOrDefault(
                c => c.Id == newRental.CustomerId);

                if (customer == null)
                return BadRequest("Customer Id is not valid");
           
            
            
            var movies = _context.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id)).ToList();

            if (movies.Count != newRental.MovieIds.Count)
                return BadRequest("One or more Movie Ids are invalid.");


            foreach(Movie movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");

                movie.NumberAvailable--;

                MovieRental rental = new MovieRental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };
                _context.MovieRentals.Add(rental);
            }

            _context.SaveChanges();
            return Ok();
            
        }
    }
}
