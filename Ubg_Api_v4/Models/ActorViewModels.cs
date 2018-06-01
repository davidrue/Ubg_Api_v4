using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ubg_Api_v4.Models
{
    public class ActorViewModels
    {
        public class RegisterViewModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            //[DataType(DataType.Password)]
            //[Display(Name = "Confirm password")]
            //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            //public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Your SurName")]
            public string SurName { get; set; }

            [Required]
            [Display(Name = "Your Name")]
            public string Name { get; set; }


            [Required]
            [Display(Name = "Your Iban")]
            public string firstIban { get; set; }

            [Required]
            [Display(Name = "Your UserName")]
            public string UserName { get; set; }

            

        }
    }
}