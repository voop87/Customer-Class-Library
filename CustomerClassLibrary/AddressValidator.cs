using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace CustomerClassLibrary 
{
    public class AddressValidator : AbstractValidator<Address>
    {

        public AddressValidator()
        {
            RuleFor(Adrress => Adrress.AdressLine).NotEmpty().WithMessage("The field is required").
                MaximumLength(100).WithMessage("Maximum length is 100 characters");

            RuleFor(Address => Address.AdressLine2).MaximumLength(100).WithMessage("Maximum length is 100 characters");

            RuleFor(Address => Address.City).NotEmpty().WithMessage("The field is required").
                MaximumLength(50).WithMessage("Maximum length is 50 characters");

            RuleFor(Address => Address.PostalCode).NotEmpty().WithMessage("The field is required").
                MaximumLength(6).WithMessage("Maximum length is 6 characters");

            RuleFor(Address => Address.State).NotEmpty().WithMessage("The field is required").
                MaximumLength(20).WithMessage("Maximum length is 20 characters");

            RuleFor(Address => Address.Country).NotEmpty().WithMessage("The field is required").
                Must(field => field == "USA" || field == "Canada").WithMessage("The field can be only USA or Canada");
        }
    }
}
