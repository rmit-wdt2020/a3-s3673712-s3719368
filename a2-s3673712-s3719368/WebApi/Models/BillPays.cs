using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class BillPays
    {
        public int BillPayId { get; set; }
        public int AccountNumber { get; set; }
        public int PayeeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime ScheduleDate { get; set; }
        public int Period { get; set; }
        public DateTime ModifyDate { get; set; }

        public virtual Accounts AccountNumberNavigation { get; set; }
        public virtual Payees Payee { get; set; }
    }
}
