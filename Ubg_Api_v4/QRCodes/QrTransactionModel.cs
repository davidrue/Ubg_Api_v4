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
            public DateTime available_until { get; set; }
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

           
            public Boolean include_qr { get; set; }
               
            public string auth_token { get; set; }

            public string user_name { get; set; }

        }


        public class PaymentConfirmationModel
        {
           
            public string refId { get; set; }
            public string auth_token { get; set; }            

        }

        public class PaymentConfirmationAnswerModel
        {

            public Boolean transfer_commissioned { get; set; }

            public Boolean adjusted_amount { get; set; }

            public decimal amount { get; set; }

        }


        public class MakePaymentModel
        {
            public decimal adjusted_amount { get; set; }           

        }

        public class AnswertAuthTokenModel
        {

            public string auth_token { get; set; }
            public DateTime ExpiryDate { get; set; }

        }



        public class GetInformationFromQRCodeModelAnswer
        {
            [Required]
            public string iban { get; set; }

            [Required]
            public Decimal amount { get; set; }

            [Required]
            public string currency { get; set; }

            [Required]
            public string reference { get; set; }

            [Required]
            public Boolean adjustible_up { get; set; }

            [Required]
            public Boolean adjustible_down { get; set; }

            public string name { get; set; }
            public string surname { get; set; }

        }

        public class PaymentHistoryModel
        {
            [Required]
            public decimal amount { get; set; }

            [Required]
            public string ref_id { get; set; }

            [Required]
            public string currency { get; set; }

            [Required]
            public string reference { get; set; }
            
            [Required]
            public Boolean receiver { get; set; }

            [Required]
            public string other_name { get; set; }

        }
    }
}