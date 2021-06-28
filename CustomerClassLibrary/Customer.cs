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
        public int CustomerId { get; set; }
        public List<Address> AdressesList { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public List<Note> Notes { get; set; }

        public decimal? TotalPurshasesAmount { get; set; }

    }
}