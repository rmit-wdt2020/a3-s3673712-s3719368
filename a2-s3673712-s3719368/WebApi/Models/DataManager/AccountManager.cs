
using a2_s3673712_s3719368.Models;
using a2_s3673712_s3719368.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace BankAPI.Models.DataManager
{
    public class AccountManager : IDataRepository<Accounts, int>
    {
        private readonly NationBankContext _context;

        public AccountManager(NationBankContext context)
        {
            _context = context;
        }

        public Accounts Get(int id)
        {
            return _context.Accounts.Find(id);
        }

        public IEnumerable<Accounts> GetAll()
        {
            return _context.Accounts.Include(c => c.Customer).ToList();
        }

        public int Add(Accounts account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();

            return account.CustomerId;
        }

        public int Delete(int id)
        {
            _context.Accounts.Remove(_context.Accounts.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, Accounts account)
        {
            _context.Update(account);
            _context.SaveChanges();

            return id;
        }
    }
}
