using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public enum AccountType
    {
        Checking = 1,
        Saving = 2
    }

    public class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Account Number"), Required]
        public int AccountNumber { get; set; }

        [Display(Name = "Type"), Required]
        public AccountType AccountType { get; set; }

        [Required]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        [Required]
        public DateTime ModifyDate { get; set; }

        public virtual List<Transaction> Transactions { get; set; }
        
        public virtual List<BillPay> BillPays { get; set; }


        //Deposit
        public void Deposit(decimal amount, bool transaction) {
            Balance += amount;
            if (transaction) {
                ModifyDate = DateTime.UtcNow;
                Transactions.Add(
               new Transaction
               {
                   TransactionType = TransactionType.Deposit,
                   Amount = amount,
                   TransactionTimeUtc = DateTime.UtcNow
               });
            }         
        }

        //Transfer 
        public bool Transfer(decimal amount, Account account, bool free ,string Comment)
        {
            if (this == account)
                return false;
            if (this.Withdraw(amount,false,true)) //no service charge for withdraw
            {
                account.Deposit(amount, false);
                //  this.Transactions.Add(new Transaction(TransactionType.T, this.AccountNumber, account.AccountNumber, amount, Comment)); //creating transfer transaction
                Transactions.Add(
                     new Transaction
                     {
                         AccountNumber = this.AccountNumber,
                         TransactionType = TransactionType.Transfer,
                         Amount = amount,
                         DestinationAccountNumber = account.AccountNumber,
                         Comment = Comment,
                         TransactionTimeUtc = DateTime.UtcNow
                     }
                     );
                if (!free)//check for service charge
                {//+0.2 for the service charge
                    Balance -= 0.2M;
                    Transactions.Add(
                         new Transaction
                         {
                             TransactionType = TransactionType.ServiceCharge,
                             Amount = 0.2M, //service charge
                             TransactionTimeUtc = DateTime.UtcNow
                         }
                        );
                }
                ModifyDate = DateTime.UtcNow;
                return true;
            }
            else
            {
                ModifyDate = DateTime.UtcNow;
                return false;
            }

        }

        //Withdraw
        public bool Withdraw(decimal amount, bool transaction, bool free) {
            //withdraw from Saving account
            if (AccountType == AccountType.Saving && Balance >= amount)
            {
                Balance -= amount;
                if (transaction)
                {
                    Transactions.Add(
                  new Transaction
                  {
                      TransactionType = TransactionType.Withdraw,
                      Amount = amount,
                      TransactionTimeUtc = DateTime.UtcNow
                  }
                  );
                    if (!free)//check for service charge
                    {
                        Balance -= 0.1M;
                        Transactions.Add(
                         new Transaction
                         {
                             TransactionType = TransactionType.ServiceCharge,
                             Amount = 0.1M, //service charge
                             TransactionTimeUtc = DateTime.UtcNow
                         }
                        );
                    }
                }
                ModifyDate = DateTime.UtcNow;
                return true;
            }
            //withdraw from saving account
            else if (AccountType == AccountType.Checking && Balance >= amount + 200)
            {
                Balance -= amount;
                if (transaction)
                {
                    Transactions.Add(
                     new Transaction
                     {
                         TransactionType = TransactionType.Withdraw,
                         Amount = amount,
                         TransactionTimeUtc = DateTime.UtcNow
                     }
                     );
                    if (!free)//check for service charge
                    {
                        Balance -= 0.1M;
                        Transactions.Add(
                         new Transaction
                         {
                             TransactionType = TransactionType.ServiceCharge,
                             Amount = 0.1M, //service charge
                             TransactionTimeUtc = DateTime.UtcNow
                         }
                        );
                    }
                }
                ModifyDate = DateTime.UtcNow;
                return true;
            }
            else {
                ModifyDate = DateTime.UtcNow;
                return false;
            }
        }

        //Pay Bill
        public bool PayBill(decimal amount) 
        {
            if (Withdraw(amount, false, true))
            {
                Transactions.Add(
                    new Transaction
                    {
                        TransactionType = TransactionType.BillPay,
                        Amount = amount,
                        TransactionTimeUtc = DateTime.UtcNow
                    }
                    );
                return true;
            }
            else 
            {
                return false;
            }
        }


    }

}
