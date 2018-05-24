using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ubg_Api_v4.Models
{
    public class Transaction
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [ForeignKey("Id")]
        public virtual Actor Recipient { get; set; }

        [Required]
        [ForeignKey("Id")]
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

        [Required]
        public string Status { get; set; }


    }
}