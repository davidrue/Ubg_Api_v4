using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Ubg_Api_v4.Models
{
    public class Actor
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string SurName { get; set; }

        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public Boolean  IsPrivate { get; set; }

        [Required]
        public Boolean IsCommercial { get; set; }

        public string AuthToken { get; set; }

        [Required]
        public string Password { get; set; }

        [JsonIgnore]
        public virtual ICollection<BankAccount> BankAccounts { get; set; }

        [JsonIgnore]
        public virtual ICollection<Transaction> Transactions { get; set; }

    }
}