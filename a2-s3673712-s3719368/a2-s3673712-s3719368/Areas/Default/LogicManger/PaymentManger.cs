using a2_s3673712_s3719368.Data;
using a2_s3673712_s3719368.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace a2_s3673712_s3719368.Controllers
{
    public class PaymentManger
    {
        private readonly NationBankContext _context;
        private Controller controller;
        private int CustomerID;

        public PaymentManger(NationBankContext context, Controller controller, int CustomerID)
        { 
            _context = context;
            this.controller = controller;
            this.CustomerID = CustomerID;
        }


        public async Task<bool> InsertPayment(int? id, int PayeeID, decimal Amount, DateTime ScheduleDate, Period Period) 
        {
            Account account = await _context.Accounts.FindAsync(id);
            if (account.Balance < Amount)
                controller.ModelState.AddModelError(nameof(Amount), "Your account balance is not enough.");

            if (!await CheckNull(id))
            {//check if the customer own this account
                controller.ModelState.AddModelError("AccountNotExsit", "Account not exsit in your name.");
            }


            if (CountDecimalPlaces(Amount) > 2)
                controller.ModelState.AddModelError(nameof(Amount), "Amount cannot have more than 2 decimal places.");
            if (Amount <= 0)
                controller.ModelState.AddModelError(nameof(Amount), "Amount must be positive.");


            if (!(CheckPayee(PayeeID)))
            {//check if the customer own this account
                controller.ModelState.AddModelError("AccountNotExsit", "Payee not exsit");
            }

            if (ScheduleDate < DateTime.Today)
            {
                controller.ModelState.AddModelError("ScheduleDate", "Schedule Date cannot be ealier than today");
            }

            if (!controller.ModelState.IsValid)
            {
                return false;
            }

            BillPay billPay = new BillPay
            {
                AccountNumber = (int)id,
                PayeeID = PayeeID,
                Amount = Amount,
                ModifyDate = DateTime.UtcNow, //default modfied today
                ScheduleDate = ScheduleDate.ToUniversalTime(),
                Period = Period
            };
            _context.BillPays.Add(billPay);
            _context.SaveChanges();

            return true;
        }

        public async Task<bool> DeletePayment(int? id) 
        {
            if (id == null)
            {
                return false; 
            }

            var billPay = await _context.BillPays
    .Include(a => a.Account)
    .FirstOrDefaultAsync(b => b.BillPayID == id);

            if (!BillPayExists((int)id))
            {
                return false;
            }

            _context.BillPays.Remove(billPay);
            await _context.SaveChangesAsync();
            return true;
        }
        public bool BillPayExists(int id)
        {
            return _context.BillPays.Any(e => e.BillPayID == id);
        }

        public async Task<bool> EditPayment(int id, [Bind("BillPayID,AccountNumber,PayeeID,Amount,ScheduleDate,Period,ModifyDate")] BillPay billPay) 
        {
            if (id != billPay.BillPayID)
            {
                return false;
            }

            if (CountDecimalPlaces(billPay.Amount) > 2)
                controller.ModelState.AddModelError(nameof(billPay.Amount), "Amount cannot have more than 2 decimal places.");
            if (billPay.Amount <= 0)
                controller.ModelState.AddModelError(nameof(billPay.Amount), "Amount must be positive.");

            if (controller.ModelState.IsValid)
            {
                try
                {
                    billPay.ScheduleDate = billPay.ScheduleDate.ToUniversalTime();
                    billPay.ModifyDate = billPay.ModifyDate.ToUniversalTime();
                    _context.Update(billPay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillPayExists(billPay.BillPayID))
                    {
                        return false;
                    }
                    else
                    {
                        throw;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
            


        }


        public async Task<bool> CheckNull(int? accountNumber)
        {
            Account account = await _context.Accounts.FindAsync(accountNumber);
            Customer customer = await _context.Customers.FindAsync(CustomerID);
            if (!(customer.Accounts.Contains(account)) || accountNumber == null)
            {//check if the customer own this account
                return false;
            }
            else
            {
                return true;
            }
        }
        //return Payee
        public bool CheckPayee(int? PayeeID)
        {
            return _context.Payees.Any(e => e.PayeeID == PayeeID);
        }

       

        //check if decimals have no more than 2 
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
