using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerClassLibrary;
using Xunit;
using FluentValidation.Results;

namespace CustomerLibraryTests
{
    public class CustomerValidatorTests
    {
        [Fact]
        public void shouldReturnErrorMessages()
        {
            Customer customer = new Customer();
            CustomerValidator customerValidator = new CustomerValidator();
            ValidationResult results = customerValidator.Validate(customer);
            List<Tuple<string, string>> errorList = new List<Tuple<string, string>>();

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    errorList.Add(new Tuple<string, string>(failure.PropertyName, failure.ErrorMessage));
                }
            }

            Assert.Equal("The field is required", errorList.FirstOrDefault(x => x.Item1 == "AdressesList").Item2);
            Assert.Equal("The field is required", errorList.FirstOrDefault(x => x.Item1 == "Note").Item2);
            

            Customer customer1 = new Customer();

            customer1.FirstName = "123456789012345678901234567890123456789012345678901234567890";
            customer1.AdressesList = new List<Address>();
            customer1.PhoneNumber = "";
            customer1.Email = "12345";
            customer1.Note = new List<string>();

            ValidationResult results1 = customerValidator.Validate(customer1);
            List<Tuple<string, string>> errorList1 = new List<Tuple<string, string>>();

            if (!results1.IsValid)
            {
                foreach (var failure in results1.Errors)
                {
                    errorList1.Add(new Tuple<string, string>(failure.PropertyName, failure.ErrorMessage));
                }

            }

             Assert.Equal("Maximum length is 50 characters", errorList1.FirstOrDefault(x => x.Item1 == "FirstName").Item2);
             Assert.Equal("The field is required", errorList1.FirstOrDefault(x => x.Item1 == "AdressesList").Item2);
             Assert.Equal("Phone Number should have E.164 standart", errorList1.FirstOrDefault(x => x.Item1 == "PhoneNumber").Item2);
             Assert.Equal("Email adrress should be valid", errorList1.FirstOrDefault(x => x.Item1 == "Email").Item2);
             Assert.Equal("The field is required", errorList1.FirstOrDefault(x => x.Item1 == "Note").Item2);



        }
    }
}
