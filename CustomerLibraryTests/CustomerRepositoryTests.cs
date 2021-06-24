using CustomerClassLibrary;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomerLibraryTests
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public void ShouldBeAbleToCreateRepository()
        {
            var customerRepository = new CustomerRepository();
            Assert.NotNull(customerRepository);
        }

        [Fact]
        public void ShouldBeAbleToCreateCustomer()
        {
            var customerRepository = new CustomerRepository();
            var customer = CreateMockCustomer();

            customerRepository.Create(customer);
        }

        [Fact]
        public void ShouldBeAbleToReadCustomer()
        {
            var customerRepository = new CustomerRepository();
            customerRepository.DeleteAll();

            var customer = CreateMockCustomer();

            customerRepository.Create(customer);

            var createdCustomer = customerRepository.Read("Petrov");

            Assert.NotNull(createdCustomer);
            Assert.Equal(customer.FirstName, createdCustomer.FirstName);
        }

        [Fact]
        public void ShouldBeAbleToUpdateCustomer()
        {
            var customerRepository = new CustomerRepository();
            var customer = CreateMockCustomer();

            customerRepository.Create(customer);
            customer.FirstName = "TEST";
            customerRepository.Update(customer);

            var createdCustomer = customerRepository.Read("Petrov");

            Assert.NotNull(createdCustomer);
            Assert.Equal("TEST", createdCustomer.FirstName);
        }

        [Fact]
        public void ShouldBeAbleToDeleteCustomer()
        {
            var customerRepository = new CustomerRepository();
            var customer = CreateMockCustomer();

            customerRepository.Create(customer);

            var createdCustomer = customerRepository.Read("Petrov");
            Assert.NotNull(createdCustomer);

            customerRepository.Delete("Petrov");
            var deletedCustomer = customerRepository.Read("Petrov");
            Assert.Null(deletedCustomer);
        }

        private Customer CreateMockCustomer()
        {
            var customer = new Customer();
            var address = new List<Address>();

            customer.FirstName = "Vasya";
            customer.LastName = "Petrov";
            customer.PhoneNumber = "86054756961";
            customer.Email = "example@mail.com";
            customer.AdressesList = address;
            customer.TotalPurshasesAmount = 303000;

            return customer;
        }
    }
}
