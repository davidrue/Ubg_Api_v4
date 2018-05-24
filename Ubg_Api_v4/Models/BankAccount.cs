using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Ubg_Api_v4.Models
{
    public class BankAccount
    {

        [Required]
        public string Id { get; set; }

        [Required]
        public string Iban { get; set; }

        public int Priority { get; set; }

        
        [ForeignKey("Id")]
        [JsonIgnore]
        public virtual Actor Actor { get; set; }
    }
}