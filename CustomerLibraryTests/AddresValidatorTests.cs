using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CustomerClassLibrary;

namespace CustomerLibraryTests
{
    public class AddresValidatorTests
    {
        [Fact]
        public void ShouldReturnErrorMessages()
        {
            Address address = new Address();
            AddressValidator addressValidator = new AddressValidator();

            List<string> errorList = addressValidator.ValidateAdress(address);

            Assert.Equal("The field is required", errorList[0]);
            Assert.Equal("The field is required", errorList[1]);
            Assert.Equal("The field is required", errorList[2]);
            Assert.Equal("The field is required", errorList[3]);
            Assert.Equal("The field is required", errorList[4]);

            Address address1 = new Address();

            address1.AdressLine = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";
            address1.AdressLine2 = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";
            address1.City = "123456789012345678901234567890123456789012345678901234567890";
            address1.PostalCode = "1234567890";
            address1.State = "12345678901234567890123456789012345678901234567890";
            address1.Country = "Russia";

            List<string> errorList1 = addressValidator.ValidateAdress(address1);

            Assert.Equal("Maximum length is 100 characters", errorList1[0]);
            Assert.Equal("Maximum length is 100 characters", errorList1[1]);
            Assert.Equal("Maximum length is 50 characters", errorList1[2]);
            Assert.Equal("Maximum length is 6 characters", errorList1[3]);
            Assert.Equal("Maximum length is 20 characters", errorList1[4]);
            Assert.Equal("The field can be only USA or Canada", errorList1[5]);
        }
    }
}
