
using a2_s3673712_s3719368.Models;
using a2_s3673712_s3719368.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;

namespace BankAPI.Models.DataManager
{
    public class AccountManager : IDataRepository<Account, int>
    {
        private readonly NationBankContext _context;

        public AccountManager(NationBankContext context)
        {
            _context = context;
        }

        public Account Get(int id)
        {
            return _context.Accounts.Find(id);
        }

        public IEnumerable<Account> GetAll()
        {
            return _context.Accounts.Include(c => c.Transactions).Include(e => e.BillPays).ToList();
        }

        public int Add(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();

            return account.CustomerID;
        }

        public int Delete(int id)
        {
            _context.Accounts.Remove(_context.Accounts.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, Account account)
        {
            _context.Update(account);
            _context.SaveChanges();

            return id;
        }
    }
}
