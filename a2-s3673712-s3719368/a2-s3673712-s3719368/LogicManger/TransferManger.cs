using a2_s3673712_s3719368.Data;
using a2_s3673712_s3719368.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace a2_s3673712_s3719368.LogicManger
{
    //Use of this class to separate all transfer method from ATMController
    public class TransferManger
    {
        private readonly NationBankContext _context;
        private Controller controller;
        private int CustomerID;
        public TransferManger(NationBankContext context, Controller controller, int CustomerID) 
        {
            _context = context;
            this.controller = controller;
            this.CustomerID = CustomerID;
        }


        public bool CheckFree(int accountID)
        { //check if the total number of transaction of W and T >= 4
            int Transactions = _context.Transactions.Where(t => t.AccountNumber == accountID
            && (t.TransactionType == TransactionType.Withdraw || t.TransactionType == TransactionType.Transfer)).Count();
            if (Transactions >= 4)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> Deposit(int FromAccount, decimal amount)
        {
            var account = await _context.Accounts.FindAsync(FromAccount);
            account.Deposit(amount, true);
            await _context.SaveChangesAsync();
            return true;
        }

        public async void WithDrawOrDepositCheck(int FromAccount, decimal amount) 
        {
            var customer = await _context.Customers.FindAsync(CustomerID);
            bool AccountExsit = false;
            foreach (Account account in customer.Accounts)
            {
                if (account.AccountNumber == FromAccount)
                {
                    AccountExsit = true;
                }
            }

            if (CountDecimalPlaces(amount) > 2)
                controller.ModelState.AddModelError(nameof(amount), "Amount cannot have more than 2 decimal places.");
            if (amount <= 0)
                controller.ModelState.AddModelError(nameof(amount), "Amount must be positive.");
            if (!AccountExsit)
            {
                controller.ModelState.AddModelError("FromAccountNotExsit", "Account not exsit in your name");
            }

        }

        public async Task<bool> TransferCheck(int FromAccount, int ToAccount) 
        {
            List<Account> accountList = _context.Accounts.ToList();
            if (!(accountList.Contains(await _context.Accounts.FindAsync(ToAccount)))) //validation
            {
                controller.ModelState.AddModelError("ToAccountNotExsit", "Account not exsit");
                return false;
            }
            if (FromAccount == ToAccount)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Withdraw(int FromAccount, decimal amount)
        {
            var account = await _context.Accounts.FindAsync(FromAccount);
            if (account.Withdraw(amount, true, CheckFree(FromAccount)))
            {
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<bool> Transfer(int FromAccount, int ToAccount, decimal amount, string Comment)
        {
            var MyAccount = await _context.Accounts.FindAsync(FromAccount);
            var TargertAccount = await _context.Accounts.FindAsync(ToAccount);
            if (MyAccount.Transfer(amount, TargertAccount, CheckFree(FromAccount), Comment))
            {
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                controller.ModelState.AddModelError("WithDrawFailed", "Transfer failed, please check the amount and your account, and try again.");
                return false;
            }

        }


        //check Decimal place for input ammount
        public decimal CountDecimalPlaces(decimal dec)
        {
            int[] bits = Decimal.GetBits(dec);
            int exponent = bits[3] >> 16;
            int result = exponent;
            long lowDecimal = bits[0] | (bits[1] >> 8);
            while ((lowDecimal % 10) == 0)
            {
                result--;
                lowDecimal /= 10;
            }

            return result;
        }

    }
}
