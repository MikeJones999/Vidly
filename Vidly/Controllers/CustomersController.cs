﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {

        private ApplicationDbContext _context;


        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Customers
        public ActionResult Index()
        {
             //prepares conection with db storing customers
             //connection made in view when object os iterated over unless .Tolist is used
             //Include - is called Eager loading - calls the necessary db to retrieve data
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

               return View(customers);

        }

        //return detailed view of specific customer
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);

        }


        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
              
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            //if customer is new and thus their id == 0
            if (customer.Id == 0)
            {
                //add customer to database
                _context.Customers.Add(customer);
            }
            else
            {
                //locate and match the customer from the DB
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);


                //long way but more secure to map new properties
                //possibly look at automapper program
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipType = customer.MembershipType;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;

                
             }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }



        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }


            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}