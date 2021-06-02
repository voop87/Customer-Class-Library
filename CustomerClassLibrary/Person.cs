using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerClassLibrary
{
    public abstract class Person
    {
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        public string FirstName {get; set;}
        [Required(ErrorMessage = "The field is required")]
        public string LastName { get; set; }
    }
}
