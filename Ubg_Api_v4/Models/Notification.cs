using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ubg_Api_v4.Models
{
    public class Notification
    {


        [JsonIgnore]
        public string Id { get; set; }

        public decimal amount { get; set; }

        public string currency { get; set; }

        public string sender_full_name { get; set; }

        public string reference { get; set; }

        [JsonIgnore]
        public string receiverId { get; set; }

        [JsonIgnore]
        public Boolean transmitted { get; set; }

    }
}