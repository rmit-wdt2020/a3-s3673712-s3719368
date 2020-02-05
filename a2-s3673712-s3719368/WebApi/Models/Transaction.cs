using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public enum TransactionType
    {
        Deposit = 'D',
        Withdraw = 'W',
        Transfer = 'T',
        ServiceCharge = 'S',
        BillPay = 'B'
    }

    public class Transaction
    {
        [Required]
        public int TransactionID { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        [Required]
        public int AccountNumber { get; set; }
    
        public virtual Account Account { get; set; }

        [ForeignKey("DestinationAccount")]
        public int? DestinationAccountNumber { get; set; }
        public virtual Account DestinationAccount { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }

        public DateTime TransactionTimeUtc { get; set; }
    }
}
