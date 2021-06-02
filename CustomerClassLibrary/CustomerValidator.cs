using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CustomerClassLibrary
{
    public class CustomerValidator
    {
        public List<string> ValidateCustomer(Customer anyCustomer)
        {
            var result = new List<string>();
            var results = new List<ValidationResult>();
            var context = new ValidationContext(anyCustomer);

            if (!Validator.TryValidateObject(anyCustomer, context, results, true))
            {
                foreach (var error in results)
                {
                    result.Add(error.ErrorMessage);
                }
            }
            return result;
        }
    }
}
