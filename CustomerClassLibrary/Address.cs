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
       
        public string AdressLine { get; set; }

        public string AdressLine2 { get; set; }

        public AddressType AddressType { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
    }
}
