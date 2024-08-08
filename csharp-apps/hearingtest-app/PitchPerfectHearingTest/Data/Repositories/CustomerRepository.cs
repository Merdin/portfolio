using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PitchPerfectHearingTest.Interfaces;
using PitchPerfectHearingTest.Models;
using System.Collections.Generic;
using System.Linq;

namespace PitchPerfectHearingTest.DataAccess.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly DbContext _context;

        public CustomerRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Create(Customer entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var sqlex = ex.InnerException.InnerException as SqlException;
                throw sqlex;
            }
        }

        public void Delete(Customer entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Set<Customer>().ToList();
        }

        public Customer GetSingle(int id)
        {
            var customers = GetAll();
            Customer customer = (Customer)(from Customer c in customers
                                            where c.CustomerId == id
                                            select c).First();
            return customer;

        }

        public void Update(Customer entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
