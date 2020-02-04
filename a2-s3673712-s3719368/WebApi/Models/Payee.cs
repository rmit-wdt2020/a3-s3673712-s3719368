using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{

    public class Payee
    {
        [Required]
        public int PayeeID { get; set; }

        [Required]
        public string PayeeName { get; set; }


        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(4)]
        public string PostCode { get; set; }

        [StringLength(3)]
        public string State { get; set; }

        [Required, RegularExpression(@"^[61][0-9]{8}$",
         ErrorMessage = "Wrong number format.")]
        public string Phone { get; set; }
        public BillPay BillPay { get; set; }
    }
}