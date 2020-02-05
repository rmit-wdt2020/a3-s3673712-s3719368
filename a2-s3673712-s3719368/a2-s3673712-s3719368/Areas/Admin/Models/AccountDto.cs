using a2_s3673712_s3719368.Areas.Admin.Models;
using a2_s3673712_s3719368.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace a2_s3673712_s3719368.Area.Admin.Models
{
    public class AccountDto
    {
        [Display(Name = "Account Number"), Required]
        public int AccountNumber { get; set; }

        [Display(Name = "Type"), Required]
        public AccountType AccountType { get; set; }

        [Required]
        public int CustomerID { get; set; }
        public virtual CustomerDto Customer { get; set; }

        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        [Required]
        public DateTime ModifyDate { get; set; }

        public virtual List<TransactionDto> Transactions { get; set; }
    }
}
