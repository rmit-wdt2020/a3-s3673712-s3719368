using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class Transactions
    {
        public int TransactionId { get; set; }
        public int TransactionType { get; set; }
        public int AccountNumber { get; set; }
        public int? DestinationAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime TransactionTimeUtc { get; set; }

        public virtual Accounts AccountNumberNavigation { get; set; }
        public virtual Accounts DestinationAccountNumberNavigation { get; set; }
    }
}
