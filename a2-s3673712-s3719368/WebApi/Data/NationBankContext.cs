using a2_s3673712_s3719368.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    public class NationBankContext : DbContext
    {
        public NationBankContext(DbContextOptions<NationBankContext> options) : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<BillPay> BillPays {get;set;}
        public DbSet<Payee> Payees {get;set;}

        protected override void OnModelCreating(ModelBuilder builder) //validation
        {
            base.OnModelCreating(builder);

            builder.Entity<Login>().HasCheckConstraint("CH_Login_LoginID", "len(LoginID) = 8").
                HasCheckConstraint("CH_Login_PasswordHash", "len(PasswordHash) = 64");
            builder.Entity<Account>().HasCheckConstraint("CH_Account_Balance", "Balance >= 0");
            builder.Entity<Transaction>().
                HasOne(x => x.Account).WithMany(x => x.Transactions).HasForeignKey(x => x.AccountNumber);
            builder.Entity<BillPay>().
    HasOne(x => x.Account).WithMany(x => x.BillPays).HasForeignKey(x => x.AccountNumber);
            builder.Entity<Transaction>().HasCheckConstraint("CH_Transaction_Amount", "Amount > 0");
         
        }

        internal Account FindAsync(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
