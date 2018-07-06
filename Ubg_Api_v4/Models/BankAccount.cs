using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Ubg_Api_v4.Models
{
    //Code First Model for the BankAccount
    public class BankAccount
    {

        [Required]
        public string Id { get; set; }

        [Required]
        public string ActorId { get; set; }

        [Required]
        public string Iban { get; set; }

        public int Priority { get; set; }

        
        [ForeignKey("ActorId")]
        [JsonIgnore]
        public virtual Actor Actor { get; set; }
    }
}