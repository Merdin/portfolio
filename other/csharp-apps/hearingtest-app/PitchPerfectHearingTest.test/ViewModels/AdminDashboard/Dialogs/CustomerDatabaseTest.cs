using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PitchPerfectHearingTest.DataAccess;
using PitchPerfectHearingTest.DataAccess.Repositories;
using PitchPerfectHearingTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PitchPerfectHearingTest.test.ViewModels.AdminDashboard.Dialogs
{
    [TestClass()]
    public class CustomerDatabaseTest
    {
        [TestMethod()]
        public void ExecuteShouldReturn()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "ShouldReturnAllCustomers").Options;

            var context = new ApplicationContext(options);
            var customerRepo = new CustomerRepository(context);

            //Act
            Seed(context);
            var result = customerRepo.GetAll();


            //Assert
            Assert.AreEqual(6, result.ToList().Count);
        }
        [TestMethod()]
        public void ExecuteShouldCreateCustomer()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "ShouldCreateCustomer").Options;

            var context = new ApplicationContext(options);
            var customerRepo = new CustomerRepository(context);
            var customer = new Customer(66, "Jamie", "jamie@mail.com", 30);

            //Act
            customerRepo.Create(customer);
            var result = customerRepo.GetAll();
            Customer singleCustomer = customerRepo.GetSingle(customer.CustomerId);
            //Assert
            Assert.AreEqual("Jamie", singleCustomer.Name);
            Assert.AreEqual("jamie@mail.com", singleCustomer.Email);
            Assert.AreEqual(66, singleCustomer.CustomerId);
            Assert.AreEqual(30, singleCustomer.Age);
        }


        private void Seed(ApplicationContext context)
        {
            IList<Customer> customers = new List<Customer>
            {
                new Customer{CustomerId = 1, Name = "Jamie Spekman", Email = "Jamie@hotmail.com",Age = 33},
                new Customer{CustomerId = 2, Name = "Eligio Castro", Email = "Eligio@hotmail.com",Age = 30},
                new Customer{CustomerId = 3, Name = "Rene Kluitenberg", Email = "Rene@hotmail.com",Age = 37},
                 new Customer{CustomerId = 4, Name = "Timo Wernars", Email = "Timo@hotmail.com",Age = 21},
                 new Customer{CustomerId = 5, Name = "Dirk van der Vuurst", Email = "Dirk@hotmail.com",Age = 19},
                 new Customer{CustomerId = 6, Name = "Merdin Kahrimanovic", Email = "Merdin@hotmail.com",Age = 26},
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

    }
}
