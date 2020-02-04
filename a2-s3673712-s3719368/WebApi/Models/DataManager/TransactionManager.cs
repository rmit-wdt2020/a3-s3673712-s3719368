using a2_s3673712_s3719368.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DataManager
{
    public class TransactionManager : IDataRepository<Transactions, int>
    {
        private readonly NationBankContext _context;

        public TransactionManager(NationBankContext context)
        {
            _context = context;
        }

        public Transactions Get(int id)
        {
            return _context.Transactions.Find(id);
        }

        public IEnumerable<Transactions> GetAll()
        {
            return _context.Transactions.Include(a => a.AccountNumberNavigation).ThenInclude(a => a.TransactionsDestinationAccountNumberNavigation).ToList();
        }

        public int Add(Transactions transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return transaction.TransactionId;
        }

        public int Delete(int id)
        {
            _context.Transactions.Remove(_context.Transactions.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, Transactions transaction)
        {
            _context.Update(transaction);
            _context.SaveChanges();

            return id;
        }
    }
}
