using a2_s3673712_s3719368.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace a2_s3673712_s3719368.Data
{
    public class SeedItems
    {
        public static void Initialize(IServiceProvider serviceProvider) {
            using var context = new NationBankContext(serviceProvider.GetRequiredService<DbContextOptions<NationBankContext>>());
            // Look for customers.
            if (context.Customers.Any())
                return; // DB has already been seeded.
            const string openingBalance = "Opening balance";
            const string format = "dd/MM/yyyy hh:mm:ss tt";
            context.Customers.AddRange( //import from Day7
    new Customer
    {
        CustomerID = 2100,
        Name = "Matthew Bolger",
        TFN = "22223333444",
        Address = "123 Fake Street",
        City = "Melbourne",
        PostCode = "3000",
        State = "VIC",
        Phone = "6112345678"
    },
    new Customer
    {
        CustomerID = 2200,
        Name = "Rodney Cocker",
        TFN = "11119999444",
        Address = "456 Real Road",
        City = "Melbourne",
        PostCode = "3005",
        State = "VIC",
        Phone = "6187654321"
    },
    new Customer
    {
        CustomerID = 2300,
        Name = "Shekhar Kalra",
        Phone = "6188888888"
    });
            context.Payees.AddRange(
            new Payee
            {
                PayeeName = "Optus",
                Phone = "6188888888"

            },
            new Payee
            {
                PayeeName = "RMIT",
                Address = "124 La Trobe St",
                City = "Melbourne",
                PostCode = "3000",
                State = "VIC",
                Phone = "6199252000"
            },
            new Payee
            {
                PayeeName = "City coucil",
                Address = "2 Vincent ave",
                City = "Melbourne",
                PostCode = "3233",
                State = "VIC",
                Phone = "6113456432"
            }
            ) ;
            context.Logins.AddRange( 
                new Login
                {
                    LoginID = "12345678",
                    CustomerID = 2100,
                    PasswordHash = "YBNbEL4Lk8yMEWxiKkGBeoILHTU7WZ9n8jJSy8TNx0DAzNEFVsIVNRktiQV+I8d2",
                    ModifyDate = DateTime.ParseExact(DateTime.UtcNow.ToString(), format, null)
                },
                new Login
                {
                    LoginID = "38074569",
                    CustomerID = 2200,
                    PasswordHash = "EehwB3qMkWImf/fQPlhcka6pBMZBLlPWyiDW6NLkAh4ZFu2KNDQKONxElNsg7V04",
                    ModifyDate = DateTime.ParseExact(DateTime.UtcNow.ToString(), format, null)
                },
                new Login
                {
                    LoginID = "17963428",
                    CustomerID = 2300,
                    PasswordHash = "LuiVJWbY4A3y1SilhMU5P00K54cGEvClx5Y+xWHq7VpyIUe5fe7m+WeI0iwid7GE",
                    ModifyDate = DateTime.ParseExact(DateTime.UtcNow.ToString(), format, null)
                });

            context.Accounts.AddRange(
                new Account
                {
                    AccountNumber = 4100,
                    AccountType = AccountType.Saving,
                    CustomerID = 2100,
                    Balance = 100,
                    ModifyDate = DateTime.ParseExact(DateTime.UtcNow.ToString(), format, null)
                },
                new Account
                {
                    AccountNumber = 4101,
                    AccountType = AccountType.Checking,
                    CustomerID = 2100,
                    Balance = 500,
                    ModifyDate = DateTime.ParseExact(DateTime.UtcNow.ToString(), format, null)
                },
                new Account
                {
                    AccountNumber = 4200,
                    AccountType = AccountType.Saving,
                    CustomerID = 2200,
                    Balance = 500.95m,
                    ModifyDate = DateTime.ParseExact(DateTime.UtcNow.ToString(), format, null)
                },
                new Account
                {
                    AccountNumber = 4300,
                    AccountType = AccountType.Checking,
                    CustomerID = 2300,
                    Balance = 1250.50m,
                    ModifyDate = DateTime.ParseExact(DateTime.UtcNow.ToString(), format, null)
                }); ;

         
            context.Transactions.AddRange(
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4100,
                    Amount = 100,
                    Comment = openingBalance,
                    TransactionTimeUtc = DateTime.ParseExact("19/12/2019 08:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4101,
                    Amount = 500,
                    Comment = openingBalance,
                    TransactionTimeUtc = DateTime.ParseExact("19/12/2019 08:30:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4200,
                    Amount = 500.95m,
                    Comment = openingBalance,
                    TransactionTimeUtc = DateTime.ParseExact("19/12/2019 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4300,
                    Amount = 1250.50m,
                    Comment = "Opening balance",
                    TransactionTimeUtc = DateTime.ParseExact("19/12/2019 10:00:00 PM", format, null)
                });

            context.SaveChanges();
        }
    }
}
