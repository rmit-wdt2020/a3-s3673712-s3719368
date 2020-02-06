using a2_s3673712_s3719368.Areas.Admin.Models;
using a2_s3673712_s3719368.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace a2_s3673712_s3719368.Areas.Admin.Models
{
    public class BillPayDto
    {
        [Required]
        public int BillPayID { get; set; }
        [Required]
        public int AccountNumber { get; set; }
        public virtual AccountDto Account { get; set; }

        [ForeignKey("PayeeID"), Required]
        public int PayeeID { get; set; }
        public virtual PayeeDto Payee { get; set; }

        [Column(TypeName = "money"),Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime ScheduleDate { get; set; }

        [Required]
        public Period Period { get; set; }


        [Required]
        public DateTime ModifyDate { get; set; }

        public bool Block { get; set; }
    }
}