using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CustomerClassLibrary;
using FluentValidation.Results;

namespace CustomerLibraryTests
{
    public class AddresValidatorTests
    {
        [Fact]
        public void ShouldReturnErrorMessages()
        {
            Address address = new Address();
            AddressValidator addressValidator = new AddressValidator();
            ValidationResult results = addressValidator.Validate(address);
            List<Tuple<string, string>> errorList = new List<Tuple<string, string>>();

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    errorList.Add(new Tuple<string, string>(failure.PropertyName, failure.ErrorMessage));
                }

            }

            Assert.Equal("The field is required", errorList.FirstOrDefault(x => x.Item1 == "AdressLine").Item2);
            Assert.Equal("The field is required", errorList.FirstOrDefault(x => x.Item1 == "City").Item2);
            Assert.Equal("The field is required", errorList.FirstOrDefault(x => x.Item1 == "PostalCode").Item2);
            Assert.Equal("The field is required", errorList.FirstOrDefault(x => x.Item1 == "State").Item2);
            Assert.Equal("The field is required", errorList.FirstOrDefault(x => x.Item1 == "Country").Item2); ;

            Address address1 = new Address();

            address1.AdressLine = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";
            address1.AdressLine2 = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";
            address1.City = "123456789012345678901234567890123456789012345678901234567890";
            address1.PostalCode = "1234567890";
            address1.State = "12345678901234567890123456789012345678901234567890";
            address1.Country = "Russia";

           ValidationResult results1 = addressValidator.Validate(address1);
           List<Tuple<string, string>> errorList1 = new List<Tuple<string, string>>();

            if (!results1.IsValid)
            {
                foreach (var failure in results1.Errors)
                {
                    errorList1.Add(new Tuple<string, string>(failure.PropertyName, failure.ErrorMessage));
                }

            }

            Assert.Equal("Maximum length is 100 characters", errorList1.FirstOrDefault(x => x.Item1 == "AdressLine").Item2);
            Assert.Equal("Maximum length is 100 characters", errorList1.FirstOrDefault(x => x.Item1 == "AdressLine2").Item2);
            Assert.Equal("Maximum length is 50 characters", errorList1.FirstOrDefault(x => x.Item1 == "City").Item2);
            Assert.Equal("Maximum length is 6 characters", errorList1.FirstOrDefault(x => x.Item1 == "PostalCode").Item2);
            Assert.Equal("Maximum length is 20 characters", errorList1.FirstOrDefault(x => x.Item1 == "State").Item2);
            Assert.Equal("The field can be only USA or Canada", errorList1.FirstOrDefault(x => x.Item1 == "Country").Item2);
        }
    }
}
