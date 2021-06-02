using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CustomerClassLibrary 
{
    public class Customer : Person
    {
        [Required(ErrorMessage = "The field is required"), 
            MinLength(1, ErrorMessage = "Minimum length is 1 item")]
        public List<Address> AdressesList { get; set; }

        [RegularExpression(@"^\+?\d{10, 14}$", ErrorMessage = "Phone Number should have E.164 standart")]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                      ErrorMessage = "Email adress should be valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field is required"), 
            MinLength(1, ErrorMessage = "Minimum length is 1 item")]
        public List<string> Note { get; set; }

        public decimal? TotalPurshasesAmount { get; set; }

    }
}
