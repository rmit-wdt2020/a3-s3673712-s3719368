using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace a2_s3673712_s3719368.Models
{
    public enum Period
    {
        Monthly = 'M',
        Quarterly = 'Q',
        Annually = 'Y',
        Once_off = 'S'
    }
    public class BillPay
    {
        [Required]
        public int BillPayID { get; set; }
        [Required]
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }

        [ForeignKey("PayeeID"), Required]
        public int PayeeID { get; set; }
        public virtual Payee Payee { get; set; }

        [Column(TypeName = "money"),Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime ScheduleDate { get; set; }

        [Required]
        public Period Period { get; set; }


        [Required]
        public DateTime ModifyDate { get; set; }


    }
}