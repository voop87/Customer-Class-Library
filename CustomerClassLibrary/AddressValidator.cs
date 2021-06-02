using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CustomerClassLibrary
{
    public class AddressValidator
    {
        public List<string> ValidateAdress(Address anyAddress)
        {
            var result = new List<string>();
            var results = new List<ValidationResult>();
            var context = new ValidationContext(anyAddress);

            if (!Validator.TryValidateObject(anyAddress, context, results, true))
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
