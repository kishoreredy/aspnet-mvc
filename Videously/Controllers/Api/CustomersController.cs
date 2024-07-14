using AutoMapper;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Videously.Dtos;
using Videously.Models;

namespace Videously.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        // GET /api/customers
        public IHttpActionResult GetCustomers()
        {
            return Ok(_context.Customers.Include(c => c.MembershipType).Select(Mapper.Map<Customer, CustomerDto>));
        }

        // GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.Include(m => m.MembershipType).SingleOrDefault(c => c.Id == id);
            if(customer == null)
            {
                return NotFound();
                //throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        // POST /api/cusomers
        [HttpPost] // if method name is PostCustomer, then no need to provide HttpPost attribute
        public IHttpActionResult PostCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.Entry(customer.MembershipType).State = EntityState.Unchanged;
            _context.SaveChanges();
            customerDto.Id = customer.Id;
            return Created(new Uri($"{Request.RequestUri}/{customerDto.Id}"), customerDto);
        }

        // PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult PutCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = _context.Customers.Include(m => m.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                return NotFound();
                //throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map(customerDto, customerInDb);
            _context.Entry(customerInDb.MembershipType).State = EntityState.Unchanged;
            
            return Ok(_context.SaveChanges());
        }

        // PUT /api/customers/
        [HttpPut]
        public IHttpActionResult PutCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = _context.Customers.Include(m => m.MembershipType).SingleOrDefault(c => c.Id == customerDto.Id);
            if (customerInDb == null)
            {
                return NotFound();
                //throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map(customerDto, customerInDb);
            _context.Entry(customerInDb.MembershipType).State = EntityState.Unchanged;
            
            return Ok(_context.SaveChanges());
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customerInDb);
            
            return Ok(_context.SaveChanges());
        }
    }
}
