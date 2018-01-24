﻿using CustomerCrud.Models;
using CustomerCrud.Repository;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace CustomerCrud.Controllers
{
    public class CustomersController : ApiController
    {
        private CustomersEntities dbContext = new CustomersEntities();
        private IUnitOfWork unitOfWork;

        public CustomersController()
        {
            unitOfWork = new UnitOfWork(dbContext);
        }

        // GET: api/Customers
        public IQueryable<Customer> GetCustomers()
        {
            return unitOfWork.CustomerRepository.GetAll();
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = unitOfWork.CustomerRepository.GetSingle(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            unitOfWork.CustomerRepository.Update(customer);
            try
            {
                unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Customers
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.CustomerRepository.Add(customer);
            unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = unitOfWork.CustomerRepository.GetSingle(id);
            if (customer == null)
            {
                return NotFound();
            }

            unitOfWork.CustomerRepository.Delete(customer);
            unitOfWork.Commit();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return unitOfWork.CustomerRepository.GetAll().Where(e => e.CustomerId == id).Count() > 0;
        }
    }
}