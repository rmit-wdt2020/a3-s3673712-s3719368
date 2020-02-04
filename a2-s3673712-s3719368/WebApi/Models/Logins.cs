using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class Logins
    {
        public string LoginId { get; set; }
        public int CustomerId { get; set; }
        public string PasswordHash { get; set; }
        public DateTime ModifyDate { get; set; }
        public bool? Lock { get; set; }
        public DateTime LockDate { get; set; }
        public int Attempt { get; set; }

        public virtual Customers Customer { get; set; }
    }
}
