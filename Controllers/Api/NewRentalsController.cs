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
            Customer customer = _context.Customers.Single(
                c => c.Id == newRental.CusomterId);

            
            var movies = _context.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id));

            foreach(Movie movie in movies)
            {
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
