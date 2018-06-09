using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Ubg_Api_v4.Models
{
    public class Transaction
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string RecipientId { get; set; }

        public string SenderId { get; set; }

        [JsonIgnore]
        [ForeignKey("RecipientId")]
        public virtual Actor Recipient { get; set; }

        [JsonIgnore]
        [ForeignKey("SenderId")]
        public virtual Actor Sender { get; set; }

        [Required]
        public Decimal Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public string Reference { get; set; }

        [Required]
        public DateTime AvailableUntil { get; set; }

        [Required]
        public Boolean AdjustibleUp { get; set; }

        [Required]
        public Boolean AdjustibleDown { get; set; }

        public Boolean Adjusted { get; set; }

        //Opened, Paid
        [Required]
        public string Status { get; set; }


    }
}