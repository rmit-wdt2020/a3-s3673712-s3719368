using a2_s3673712_s3719368.Area.Admin.Models;
using a2_s3673712_s3719368.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace a2_s3673712_s3719368.Areas.Admin.Models
{
    public class TransactionDto
    {
        [Required]
        public int TransactionID { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        [Required]
        public int AccountNumber { get; set; }
        public virtual AccountDto Account { get; set; }

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
