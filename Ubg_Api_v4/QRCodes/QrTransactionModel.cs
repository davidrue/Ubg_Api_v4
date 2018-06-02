using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Ubg_Api_v4.QRCodes
{
    public class QrTransactionModel
    {
        public class QrLinKRefViewModel
        {
            public string link { get; set; }
            public string refId { get; set; }
            public string img { get; set; }
        }

        public class RequestQrCodeViewModel
        {
            [Required]
            public decimal amount { get; set; }

            [Required]
            public string currency { get; set; }

            [Required]
            public string reference { get; set; }

            [Required]
            public int available_until { get; set; }

            [Required]
            public Boolean adjustible_up { get; set; }

            [Required]
            public Boolean adjustible_down { get; set; }

            [Required]
            public Boolean include_qr { get; set; }

            [Required]
            public string auth_token { get; set; }

            [Required]
            public string user_name { get; set; }

        }




    }
}