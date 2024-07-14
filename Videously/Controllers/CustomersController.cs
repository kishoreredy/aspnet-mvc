using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Videously.Models;
using Videously.ViewModels;

namespace Videously.Controllers
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
        public ViewResult Index()
        {
            // it called via WebAPI in jquery datatable
            //var customers = GetCustomers();
            //return View(customers);
            return View();
        }

        private List<Customer> GetCustomers()
        {
            // this is called deferred execution
            // ET framework will execute the below line only from the view
            //return _context.Customers;

            // this below line of code will be called immediately as there is method call i.e., .ToList()
            return _context.Customers.Include(c => c.MembershipType).ToList();
            
            //return new List<Customer>
            //{
            //    new Customer{Id=1,Name="Yella"},
            //    new Customer{Id=2,Name="Pulla"},
            //};
        }

        public ActionResult Details(int id)
        {
            try
            {
                var customer = GetCustomers()[id - 1];
                if (customer == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(customer);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return HttpNotFound($"Customer does exist with index: {id}");
            }
        }

        public ActionResult AddCustomer()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var customerForm = new CustomerFormViewModel
            {
                Customer = new Customer(), // to remove Customer's Id field in validation summary
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", customerForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var customerForm = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList(),
                };
                return View("CustomerForm", customerForm);
            }

            MapMembershipTypeBasedOnMembershipTypeIdToCustomer(customer);
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = GetCustomers()[customer.Id - 1];
                // can be a potential reason for security breach
                //bool status = TryUpdateModel<Customer>(customer);

                Mapper.Map<Customer, Customer>(customer, customerInDb); //can be used CustomerDto. Dto -> Data Transfer Object
            }
            _context.SaveChanges();
            return RedirectToAction("Index","Customers");
        }

        private void MapMembershipTypeBasedOnMembershipTypeIdToCustomer(Customer customer)
        {
            customer.MembershipType = _context.MembershipTypes.SingleOrDefault(m => m.Id == customer.MembershipType.Id);
        }

        public ActionResult EditCustomer(int id)
        {
            try
            {
                var customer = GetCustomers()[id - 1];
                if (customer == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    var customerForm = new CustomerFormViewModel
                    {
                        Customer = customer,
                        MembershipTypes = _context.MembershipTypes.ToList(),
                    };
                    return View("CustomerForm", customerForm);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return HttpNotFound($"Customer does exist with index: {id}");
            }
        }
    }
}