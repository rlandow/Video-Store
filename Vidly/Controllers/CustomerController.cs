﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        private VidlyDataContext _context;

        public CustomerController()
        {
            _context = new VidlyDataContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }

        public ActionResult New()
        {
            var membershiptypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel {
                MembershipTypes = membershiptypes
            };


            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0) _context.Customers.Add(customer);
            else
            {
                //Mapper.Map(customer, customerInDb);
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;

            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customer");

        }

        public ActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList() ;

            //var customers = GetCustomers();
            //List<Customer> customer = new List<Customer> {
            //    //new Customer {Id = 2, Name = "Jones, Fred"},
            //    //new Customer { Id = 1, Name = "Smith, Joe" }
            //};
            return View(customers);
        }

        public ActionResult Details(int id = 0)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            //var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            if (customer == null) return HttpNotFound();

            return View(customer);

        }
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null) return HttpNotFound();
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
        public ActionResult Display(int id)
        {
            Customer customer = new Customer { Id = id, Name = "jones" };
            return View(customer);

        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer {Id = 2, Name="John Smith"},
                new Customer {Id = 1, Name="Mary Williams"}
            };
        }
    }
}