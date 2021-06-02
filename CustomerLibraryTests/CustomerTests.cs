using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerClassLibrary;
using Xunit;

namespace CustomerLibraryTests
{
    public class CustomerTests
    {
        [Fact]
        public void ShouldCreateCustomer()
        {
            Customer customer = new Customer();
            Assert.Null(customer.FirstName);
            Assert.Null(customer.LastName);
            Assert.Null(customer.PhoneNumber);
            Assert.Null(customer.Email);
            Assert.Null(customer.Note);
            Assert.Null(customer.TotalPurshasesAmount);

        }

        [Fact]
        public void ShouldSetProperties()
        {
            Customer customer = new Customer();

            var adress = new List<Address>();

            customer.FirstName = "Harry";
            customer.LastName = "Potter";
            customer.PhoneNumber = "(605) 475-6961";
            customer.Email = "hogwards@mail.com";
            customer.AdressesList = adress;
            customer.TotalPurshasesAmount = 303000;

            Assert.Equal("Harry", customer.FirstName);
            Assert.Equal("Potter", customer.LastName);
            Assert.Equal("(605) 475-6961", customer.PhoneNumber);
            Assert.Equal("hogwards@mail.com", customer.Email);
            Assert.Equal("hogwards@mail.com", customer.Email);
            Assert.Equal(adress, customer.AdressesList);
            Assert.Equal(303000, customer.TotalPurshasesAmount);

            Customer customer1 = new Customer();
            customer1.TotalPurshasesAmount = null;
            Assert.Null(customer1.TotalPurshasesAmount);

        }
    }

}
