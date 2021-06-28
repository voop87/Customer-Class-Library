using CustomerClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomerLibraryTests
{
    public class AddressRepositoryTests
    {
        [Fact]
        public void ShouldBeAbleToCreateRepository()
        {
            var addressRepository = new AddressRepository();
            Assert.NotNull(addressRepository);
        }

        [Fact]
        public void ShouldBeAbleToCreateCustomer()
        {
            var customerRepository = new CustomerRepository();
            var addressRepository = new AddressRepository();
            customerRepository.DeleteAll();
            addressRepository.DeleteAll();

            var customer = CreateMockCustomer();
            var address = CreateMockAddress();
        }

        [Fact]
        public void ShouldBeAbleToReadCustomer()
        {
            var customerRepository = new CustomerRepository();
            var addressRepository = new AddressRepository();
            customerRepository.DeleteAll();
            addressRepository.DeleteAll();
            
            var customer = CreateMockCustomer();
            var address = CreateMockAddress();

            customerRepository.Create(customer);
            addressRepository.Create(address);

            var createdAddress = addressRepository.Read(address);

            Assert.NotNull(createdAddress);
            Assert.Equal(address.State, createdAddress.State);
        }

        [Fact]
        public void ShouldBeAbleToUpdateCustomer()
        {
            var customerRepository = new CustomerRepository();
            var addressRepository = new AddressRepository();
            customerRepository.DeleteAll();
            addressRepository.DeleteAll();

            var customer = CreateMockCustomer();
            var address = CreateMockAddress();

            customerRepository.Create(customer);
            addressRepository.Create(address);

            address.City = "TEST";
            addressRepository.Update(address);

            var createdAddress = addressRepository.Read(address);

            Assert.NotNull(createdAddress);
            Assert.Equal("TEST", createdAddress.City);
        }

        [Fact]
        public void ShouldBeAbleToDeleteCustomer()
        {
            var addressRepository = new AddressRepository();
            var address = CreateMockAddress();

            addressRepository.Create(address);

            var createdAddress = addressRepository.Read(address);
            Assert.NotNull(createdAddress);

            addressRepository.Delete(address);
            var deletedCustomer = addressRepository.Read(address);
            Assert.Null(deletedCustomer);
        }

        private Address CreateMockAddress()
        {
            var address = new Address();

            address.AdressLine = "Street 1";
            address.AdressLine2 = "House 3";
            address.AddressType = AddressType.Billing;
            address.City = "Los Angeles";
            address.PostalCode = "303000";
            address.State = "LA";
            address.Country = "USA";

            return address;
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
