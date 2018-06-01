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
            public string email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string password { get; set; }

            //[DataType(DataType.Password)]
            //[Display(Name = "Confirm password")]
            //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            //public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Your SurName")]
            public string sur_name { get; set; }

            [Required]
            [Display(Name = "Your Name")]
            public string name { get; set; }


            [Required]
            [Display(Name = "Your Iban")]
            public string first_iban { get; set; }

            [Required]
            [Display(Name = "Your UserName")]
            public string user_name { get; set; }

         }
        public class RequestTokenViewModel
        {
            

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string password { get; set; }
            
            [Required]
            [Display(Name = "Your UserName")]
            public string user_name { get; set; }

        }
    }
}