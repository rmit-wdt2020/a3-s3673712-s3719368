using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class Accounts
    {
        public Accounts()
        {
            BillPays = new HashSet<BillPays>();
            TransactionsAccountNumberNavigation = new HashSet<Transactions>();
            TransactionsDestinationAccountNumberNavigation = new HashSet<Transactions>();
        }

        public int AccountNumber { get; set; }
        public int AccountType { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public DateTime ModifyDate { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual ICollection<BillPays> BillPays { get; set; }
        public virtual ICollection<Transactions> TransactionsAccountNumberNavigation { get; set; }
        public virtual ICollection<Transactions> TransactionsDestinationAccountNumberNavigation { get; set; }
    }
}
