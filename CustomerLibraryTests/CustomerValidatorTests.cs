using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerClassLibrary;
using Xunit;

namespace CustomerLibraryTests
{
    public class CustomerValidatorTests
    {
        [Fact]
        public void shouldReturnErrorMessages()
        {
            Customer customer = new Customer();
            CustomerValidator customerValidator = new CustomerValidator();
            List<string> errorList = customerValidator.ValidateCustomer(customer);

            Assert.Equal("The field is required", errorList[0]);
            Assert.Equal("The field is required", errorList[1]);
            Assert.Equal("The field is required", errorList[2]);
            

            Customer customer1 = new Customer();

            //customer1.FirstName = "123456789012345678901234567890123456789012345678901234567890";
            //customer1.LastName = "Vasya";
            customer1.AdressesList = new List<Address>();
            customer1.PhoneNumber = "12345";
            customer1.Email = "12345";
            customer1.Note = new List<string>();

            List<string> errorList1 = customerValidator.ValidateCustomer(customer1);

            Assert.Equal("Minimum length is 1 item", errorList1[0]);
            Assert.Equal("Phone Number should have E.164 standart", errorList1[1]);
            Assert.Equal("Email adress should be valid", errorList1[2]);
            Assert.Equal("Minimum length is 1 item", errorList1[3]);



        }
    }
}
