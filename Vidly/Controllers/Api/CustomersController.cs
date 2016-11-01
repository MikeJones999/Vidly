using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        //set up new connection to DB to get customers
        private ApplicationDbContext _context;


        public CustomersController()
        {
            _context = new Models.ApplicationDbContext();
        }


        // convention used: GET - /api/customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        /**
         * Get Single Customer based upon their Id
         * */
        public Customer GetCustomer(int id)
        {
            var customer =  _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return customer;
        }


        //POST /api/customers
        /**
         * Creates and returns newly created customer
         * */
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }


        //PUT /api/customer/1
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            //validate input
            if(!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            //check that the customer ID exists
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            //customer found - now update
            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;

            _context.SaveChanges();
        }


        //Delete /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            //customer found - now remove
            _context.Customers.Remove(customer);
            _context.SaveChanges();


        }

    }
}
