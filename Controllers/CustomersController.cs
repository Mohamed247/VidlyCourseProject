using System;
using System.Collections.Generic;
using System.Data.Entity; //include(c => c.MembershipType;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using VidlyProject.Models;
using VidlyProject.ViewModels;

namespace VidlyProject.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            //need to initialize _context in the constructor to be able to access the db
            _context = new ApplicationDbContext();   //disposable object so I must dispose of it

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Edit(int id)
        {
            Customer customer = _context.Customers.ToList().SingleOrDefault(c => c.Id == id);

            if (customer == null) 
                return HttpNotFound();

            CustomerFormViewModel viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel); //Need to choose another different view name, second param is arg for New view
        }
        public ActionResult New()
        {
            IEnumerable<MembershipType> membershipTypes = _context.MembershipTypes.ToList();

            var ViewModel = new CustomerFormViewModel()
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            
            return View("CustomerForm", ViewModel);
        }

        [HttpPost]  //make sure not httpget, if modify data, never let it be httpget
        [ValidateAntiForgeryToken] //to help in validation where it encrypts and validates the token
        public ActionResult Save(CustomerFormViewModel viewModel) //or use updatecustomerdto (small class we create with only properties we want to update
        {
            if (!ModelState.IsValid) //check if object is valid according to the data annotations specified
            {
                CustomerFormViewModel viewModel1 = new CustomerFormViewModel
                {
                    Customer = viewModel.Customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel1);
            }
            if (viewModel.Customer.Id == 0)
            {
                _context.Customers.Add(viewModel.Customer);
            }
            else
            {
                Customer customerInDb = _context.Customers.Single(c => c.Id == viewModel.Customer.Id); //customer object in db
                                                                                                       //need to update its properties to be like those in viemodelcustomer

                //TryUpdateModel(customerInDb,"",new string[] { "Name", "Id" }); //dont use this approach since security conecerns
                //name id is whitelisted as the only things to be updated
                //OR
                //Mapper.Map(customer, customerInDb)

                customerInDb.Name = viewModel.Customer.Name; 
                customerInDb.Birthdate = viewModel.Customer.Birthdate;
                customerInDb.MembershipTypeId = viewModel.Customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = viewModel.Customer.IsSubscribedToNewsletter;

            }

            _context.SaveChanges(); //must save changes after creating a change in the database

            return RedirectToAction("Index", "Customers");
        }
        // GET: Customers
        public ActionResult Index()
        {
            //var customers = GetCustomers();

            // var customers = _context.Customers.Include(c => c.MembershipType).ToList(); //must be ToList so that the query takes place (query takes place only if iterated upon)
            //return View(customers);

            //we want to cache the list of genres
            //if (MemoryCache.Default["Genres"] == null)  //every item we store in a cache, we use a key to acces it
            //{
            //    //get genres from the database
            //    MemoryCache.Default["Genres"] = _context.Genres.ToList();
            //}

            //var genres = MemoryCache.Default["Genres"] as IEnumerable<Genre>;  //simple casting, can use normal casting instead
            return View();
        }

        
        [Route("Customers/Details/{id}")]

        public ActionResult GetDetails(int id)
        {
            //var customer = GetCustomers().SingleOrDefault(customerTemp => customerTemp.Id == id);
            var customer = _context.Customers.Include(c => c.MembershipType).ToList().SingleOrDefault(c => c.Id == id);
            if (customer == null) return HttpNotFound();

            return View(customer);
        }
        private IEnumerable<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>()
            {
                new Customer(){ Name = "John Smith", Id = 1},
                new Customer(){ Name = "Mary Williams", Id = 2}
            };
            return customers;
        }
        //public ActionResult ViewCustomers()
        //{
        //    var customers = _context.Customers.ToList();

        //    return View(customers);
        //}

    }
}