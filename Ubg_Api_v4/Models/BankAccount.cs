using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ubg_Api_v4.Models
{
    public class BankAccount
    {

        [Required]
        public string Id { get; set; }

        [Required]
        public string Iban { get; set; }


        // Foreign Key
        public string Actor_Id { get; set; }
    }
}