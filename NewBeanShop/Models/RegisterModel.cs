using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewBeanShop.Models
{
    public class RegisterModel
    {


        [Required]
        [EmailAddress]
        public string UserName { get; set; }


        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must between 8-100 chars")]
        public string Password { get; set; }



        [Required]
        [Compare("Password", ErrorMessage = "The password does not match")]
        public string ConfirmPassword { get; set; }

        //---bellow properties are just for our user table, not for identity
        //---we will sort them at the controller, passing these to the user and just the above
        //---to the identity framework


        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }
    }
}