using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CustomerClassLibrary
{
    public enum AddressType
    {
        Shipping,
        Billing
    }
    public enum Countries
    {
        USA,
        Canada
    }

    public class Address
    {
        [Required(ErrorMessage = "The field is required"), 
            MaxLength(100, ErrorMessage = "Maximum length is 100 characters")]

        public string AdressLine { get; set; }

        [MaxLength(100, ErrorMessage = "Maximum length is 100 characters")]
        public string AdressLine2 { get; set; }

        public AddressType AddressType { get; set; }

        [Required(ErrorMessage = "The field is required"), MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "The field is required"), 
            MaxLength(6, ErrorMessage = "Maximum length is 6 characters")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "The field is required"), 
            MaxLength(20, ErrorMessage = "Maximum length is 20 characters")]
        public string State { get; set; }

        [Required(ErrorMessage = "The field is required"), 
            USAorCanada(ErrorMessage = "The field can be only USA or Canada")]
        public string Country { get; set; }
    }
}
